using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioOutput : MonoBehaviour {

    public SynthModule signalIn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (signalIn != null)
        {
            signalIn.ProcessBuffer(data, channels);
        }
    }
}
