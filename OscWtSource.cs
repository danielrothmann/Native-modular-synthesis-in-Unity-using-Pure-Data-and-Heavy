using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hv_osc_wavetable_single_AudioLib))]
public class OscWtSource : SynthModule {

    Hv_osc_wavetable_single_AudioLib osc;
    public AudioClip defaultWaveTable;

    public SynthModule cvFreqIn;
    public SynthModule cvAmpIn;
    float[] cvFreqBuffer = new float[2048];
    float[] cvAmpBuffer = new float[2048];
    float[] mergedBuffer = new float[2048];
    int totalChannels = 4;

    // Use this for initialization
    void Start () {
        Type = ModuleType.Source;

        osc = GetComponent<Hv_osc_wavetable_single_AudioLib>();
        osc.FillTableWithMonoAudioClip("waveform", defaultWaveTable);
    }

    public override void ProcessBuffer(float[] buffer, int numChannels)
    {
        // Process CV inputs if they have been connected
        if (cvFreqIn != null)
        {
            if (cvFreqBuffer.Length != buffer.Length)
            {
                cvFreqBuffer = new float[buffer.Length];
            }

            cvFreqIn.ProcessBuffer(cvFreqBuffer, numChannels);
        }
        if (cvAmpIn != null)
        {
            if (cvAmpBuffer.Length != buffer.Length)
            {
                cvAmpBuffer = new float[buffer.Length];
            }

            cvAmpIn.ProcessBuffer(cvAmpBuffer, numChannels);
        }

        // Set total channels to buffer channels plus 2 CV input channels
        totalChannels = numChannels + 2;

        // Resize buffer if does not have correct length
        if (mergedBuffer.Length != buffer.Length / numChannels * totalChannels)
            mergedBuffer = new float[buffer.Length / numChannels * totalChannels];
        
        // Combine audio and CV inputs into a shared buffer to be processed
        for (int i = 0; i < mergedBuffer.Length / totalChannels; i++)
        {
            for (int channel = 0; channel < totalChannels; channel++)
            {
                if (channel < numChannels)
                {
                    mergedBuffer[i * totalChannels + channel] = 1f;
                }
                else if (channel < numChannels + 1)
                {
                    if(cvFreqIn != null)
                        mergedBuffer[i * totalChannels + channel] = cvFreqBuffer[i / totalChannels];
                    else
                        mergedBuffer[i * totalChannels + channel] = 0f;
                }
                else if (channel < numChannels + 2)
                {
                    if (cvAmpIn != null)
                        mergedBuffer[i * totalChannels + channel] = cvAmpBuffer[i / totalChannels];
                    else
                        mergedBuffer[i * totalChannels + channel] = 0f;
                }
            }
        }

        // Process buffer and flag it as processed for this block
        osc.ProcessBuffer(mergedBuffer, totalChannels);

        // Extract audio channels from merged buffer
        for (int i = 0; i < mergedBuffer.Length / totalChannels; i++)
        {
            for (int channel = 0; channel < numChannels; channel++)
            {
                    buffer[i * numChannels + channel] = mergedBuffer[i * totalChannels + channel];
            }
        }
    }

    public enum Parameter { Freq_Coarse, Freq_Fine, Amp, Cv_Freq_Amt, Cv_Amp_Amt }

    public override void SetParameter(int paramIndex, float value)
    {
        switch(paramIndex)
        {
            case (int) Parameter.Freq_Coarse:
                osc.SetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Freqcoarse, value);
                break;
            case (int) Parameter.Freq_Fine:
                osc.SetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Freqfine, value);
                break;
            case (int) Parameter.Amp:
                osc.SetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Amp, value);
                break;
            case (int) Parameter.Cv_Freq_Amt:
                osc.SetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Cvfreqamt, value);
                break;
            case (int)Parameter.Cv_Amp_Amt:
                osc.SetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Cvampamt, value);
                break;
        }
    }

    public override float GetParameter(int paramIndex)
    {
        switch (paramIndex)
        {
            case (int)Parameter.Freq_Coarse:
                return osc.GetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Freqcoarse);
            case (int)Parameter.Freq_Fine:
                return osc.GetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Freqfine);
            case (int)Parameter.Amp:
                return osc.GetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Amp);
            case (int)Parameter.Cv_Freq_Amt:
                return osc.GetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Cvfreqamt);
            case (int)Parameter.Cv_Amp_Amt:
                return osc.GetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Cvampamt);
            default: return 0.0f;
        }
    }

    public void SetWavetable(AudioClip wavetable)
    {
        osc.FillTableWithMonoAudioClip("waveform", wavetable);
    }

    public void SetWavetable(float[] wavetable)
    {
        osc.FillTableWithFloatBuffer("waveform", wavetable);
    }
}
