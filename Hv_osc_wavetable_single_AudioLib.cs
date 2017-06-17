/**
 * Copyright (c) 2017 Enzien Audio, Ltd.
 * 
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions, and the following disclaimer.
 * 
 * 2. Redistributions in binary form must reproduce the phrase "powered by heavy",
 *    the heavy logo, and a hyperlink to https://enzienaudio.com, all in a visible
 *    form.
 * 
 *   2.1 If the Application is distributed in a store system (for example,
 *       the Apple "App Store" or "Google Play"), the phrase "powered by heavy"
 *       shall be included in the app description or the copyright text as well as
 *       the in the app itself. The heavy logo will shall be visible in the app
 *       itself as well.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 * 
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions;
using AOT;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Hv_osc_wavetable_single_AudioLib))]
public class Hv_osc_wavetable_single_Editor : Editor {

  [MenuItem("Heavy/osc_wavetable_single")]
  static void CreateHv_osc_wavetable_single() {
    GameObject target = Selection.activeGameObject;
    if (target != null) {
      target.AddComponent<Hv_osc_wavetable_single_AudioLib>();
    }
  }
  
  private Hv_osc_wavetable_single_AudioLib _dsp;

  private void OnEnable() {
    _dsp = target as Hv_osc_wavetable_single_AudioLib;
  }

  public override void OnInspectorGUI() {
    bool isEnabled = _dsp.IsInstantiated();
    if (!isEnabled) {
      EditorGUILayout.LabelField("Press Play!",  EditorStyles.centeredGreyMiniLabel);
    }
    GUILayout.EndVertical();

    // parameters
    GUI.enabled = true;
    GUILayout.BeginVertical();
    EditorGUILayout.Space();
    EditorGUI.indentLevel++;
    
    // amp
    GUILayout.BeginHorizontal();
    float amp = _dsp.GetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Amp);
    float newAmp = EditorGUILayout.Slider("amp", amp, 0.0f, 1.0f);
    if (amp != newAmp) {
      _dsp.SetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Amp, newAmp);
    }
    GUILayout.EndHorizontal();
    
    // cvAmpAmt
    GUILayout.BeginHorizontal();
    float cvAmpAmt = _dsp.GetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Cvampamt);
    float newCvampamt = EditorGUILayout.Slider("cvAmpAmt", cvAmpAmt, 0.0f, 1.0f);
    if (cvAmpAmt != newCvampamt) {
      _dsp.SetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Cvampamt, newCvampamt);
    }
    GUILayout.EndHorizontal();
    
    // cvFreqAmt
    GUILayout.BeginHorizontal();
    float cvFreqAmt = _dsp.GetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Cvfreqamt);
    float newCvfreqamt = EditorGUILayout.Slider("cvFreqAmt", cvFreqAmt, 0.0f, 1.0f);
    if (cvFreqAmt != newCvfreqamt) {
      _dsp.SetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Cvfreqamt, newCvfreqamt);
    }
    GUILayout.EndHorizontal();
    
    // freqCoarse
    GUILayout.BeginHorizontal();
    float freqCoarse = _dsp.GetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Freqcoarse);
    float newFreqcoarse = EditorGUILayout.Slider("freqCoarse", freqCoarse, 0.0f, 1.0f);
    if (freqCoarse != newFreqcoarse) {
      _dsp.SetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Freqcoarse, newFreqcoarse);
    }
    GUILayout.EndHorizontal();
    
    // freqFine
    GUILayout.BeginHorizontal();
    float freqFine = _dsp.GetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Freqfine);
    float newFreqfine = EditorGUILayout.Slider("freqFine", freqFine, 0.0f, 1.0f);
    if (freqFine != newFreqfine) {
      _dsp.SetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Freqfine, newFreqfine);
    }
    GUILayout.EndHorizontal();
    
    // lfoOn
    GUILayout.BeginHorizontal();
    float lfoOn = _dsp.GetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Lfoon);
    float newLfoon = EditorGUILayout.Slider("lfoOn", lfoOn, 0.0f, 1.0f);
    if (lfoOn != newLfoon) {
      _dsp.SetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Lfoon, newLfoon);
    }
    GUILayout.EndHorizontal();
    EditorGUI.indentLevel--;
  }
}
#endif // UNITY_EDITOR

public class Hv_osc_wavetable_single_AudioLib : MonoBehaviour {
  
  // Parameters are used to send float messages into the patch context (thread-safe).
  // Example usage:
  /*
    void Start () {
        Hv_osc_wavetable_single_AudioLib script = GetComponent<Hv_osc_wavetable_single_AudioLib>();
        // Get and set a parameter
        float amp = script.GetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Amp);
        script.SetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter.Amp, amp + 0.1f);
    }
  */
  public enum Parameter : uint {
    Amp = 0x4FE9DED6,
    Cvampamt = 0x28D5F017,
    Cvfreqamt = 0xDE0D465E,
    Freqcoarse = 0xBAA829FD,
    Freqfine = 0x6EB03FC,
    Lfoon = 0x3D3157E8,
  }
  
  // Delegate method for receiving float messages from the patch context (thread-safe).
  // Example usage:
  /*
    void Start () {
        Hv_osc_wavetable_single_AudioLib script = GetComponent<Hv_osc_wavetable_single_AudioLib>();
        script.RegisterSendHook();
        script.FloatReceivedCallback += OnFloatMessage;
    }

    void OnFloatMessage(Hv_osc_wavetable_single_AudioLib.FloatMessage message) {
        Debug.Log(message.receiverName + ": " + message.value);
    }
  */
  public class FloatMessage {
    public string receiverName;
    public float value;

    public FloatMessage(string name, float x) {
      receiverName = name;
      value = x;
    }
  }
  public delegate void FloatMessageReceived(FloatMessage message);
  public FloatMessageReceived FloatReceivedCallback;
  public float amp = 0.5f;
  public float cvAmpAmt = 0.0f;
  public float cvFreqAmt = 0.0f;
  public float freqCoarse = 0.5f;
  public float freqFine = 0.5f;
  public float lfoOn = 0.0f;

  // internal state
  private Hv_osc_wavetable_single_Context _context;

  public bool IsInstantiated() {
    return (_context != null);
  }

  public void RegisterSendHook() {
    _context.RegisterSendHook();
  }
  
  // see Hv_osc_wavetable_single_AudioLib.Parameter for definitions
  public float GetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter param) {
    switch (param) {
      case Parameter.Amp: return amp;
      case Parameter.Cvampamt: return cvAmpAmt;
      case Parameter.Cvfreqamt: return cvFreqAmt;
      case Parameter.Freqcoarse: return freqCoarse;
      case Parameter.Freqfine: return freqFine;
      case Parameter.Lfoon: return lfoOn;
      default: return 0.0f;
    }
  }

  public void SetFloatParameter(Hv_osc_wavetable_single_AudioLib.Parameter param, float x) {
    switch (param) {
      case Parameter.Amp: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        amp = x;
        break;
      }
      case Parameter.Cvampamt: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        cvAmpAmt = x;
        break;
      }
      case Parameter.Cvfreqamt: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        cvFreqAmt = x;
        break;
      }
      case Parameter.Freqcoarse: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        freqCoarse = x;
        break;
      }
      case Parameter.Freqfine: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        freqFine = x;
        break;
      }
      case Parameter.Lfoon: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        lfoOn = x;
        break;
      }
      default: return;
    }
    if (IsInstantiated()) _context.SendFloatToReceiver((uint) param, x);
  }
  
  public void FillTableWithMonoAudioClip(string tableName, AudioClip clip) {
    if (clip.channels > 1) {
      Debug.LogWarning("Hv_osc_wavetable_single_AudioLib: Only loading first channel of '" +
          clip.name + "' into table '" +
          tableName + "'. Multi-channel files are not supported.");
    }
    float[] buffer = new float[clip.samples]; // copy only the 1st channel
    clip.GetData(buffer, 0);
    _context.FillTableWithFloatBuffer(tableName, buffer);
  }

  public void FillTableWithFloatBuffer(string tableName, float[] buffer) {
    _context.FillTableWithFloatBuffer(tableName, buffer);
  }

  private void Awake() {
    _context = new Hv_osc_wavetable_single_Context((double) AudioSettings.outputSampleRate);
  }
  
  private void Start() {
    _context.SendFloatToReceiver((uint) Parameter.Amp, amp);
    _context.SendFloatToReceiver((uint) Parameter.Cvampamt, cvAmpAmt);
    _context.SendFloatToReceiver((uint) Parameter.Cvfreqamt, cvFreqAmt);
    _context.SendFloatToReceiver((uint) Parameter.Freqcoarse, freqCoarse);
    _context.SendFloatToReceiver((uint) Parameter.Freqfine, freqFine);
    _context.SendFloatToReceiver((uint) Parameter.Lfoon, lfoOn);
  }
  
  private void Update() {
    // retreive sent messages
    if (_context.IsSendHookRegistered()) {
      Hv_osc_wavetable_single_AudioLib.FloatMessage tempMessage;
      while ((tempMessage = _context.msgQueue.GetNextMessage()) != null) {
        FloatReceivedCallback(tempMessage);
      }
    }
  }

  //private void OnAudioFilterRead(float[] buffer, int numChannels) {
  //  Assert.AreEqual(numChannels, _context.GetNumOutputChannels()); // invalid channel configuration
  //  _context.Process(buffer, buffer.Length / numChannels); // process dsp
  //}

    public void ProcessBuffer(float[] buffer, int numChannels)
    {
        Assert.AreEqual(numChannels, _context.GetNumOutputChannels()); // invalid channel configuration
        _context.Process(buffer, buffer.Length / numChannels); // process dsp
    }
}

class Hv_osc_wavetable_single_Context {

#if UNITY_IOS && !UNITY_EDITOR
  private const string _dllName = "__Internal";
#else
  private const string _dllName = "Hv_osc_wavetable_single_AudioLib";
#endif

  // Thread-safe message queue
  public class SendMessageQueue {
    private readonly object _msgQueueSync = new object();
    private readonly Queue<Hv_osc_wavetable_single_AudioLib.FloatMessage> _msgQueue = new Queue<Hv_osc_wavetable_single_AudioLib.FloatMessage>();

    public Hv_osc_wavetable_single_AudioLib.FloatMessage GetNextMessage() {
      lock (_msgQueueSync) {
        return (_msgQueue.Count != 0) ? _msgQueue.Dequeue() : null;
      }
    }

    public void AddMessage(string receiverName, float value) {
      Hv_osc_wavetable_single_AudioLib.FloatMessage msg = new Hv_osc_wavetable_single_AudioLib.FloatMessage(receiverName, value);
      lock (_msgQueueSync) {
        _msgQueue.Enqueue(msg);
      }
    }
  }

  public readonly SendMessageQueue msgQueue = new SendMessageQueue();
  private readonly GCHandle gch;
  private readonly IntPtr _context; // handle into unmanaged memory
  private SendHook _sendHook = null;

  [DllImport (_dllName)]
  private static extern IntPtr hv_osc_wavetable_single_new_with_options(double sampleRate, int poolKb, int inQueueKb, int outQueueKb);

  [DllImport (_dllName)]
  private static extern int hv_processInlineInterleaved(IntPtr ctx,
      [In] float[] inBuffer, [Out] float[] outBuffer, int numSamples);

  [DllImport (_dllName)]
  private static extern void hv_delete(IntPtr ctx);

  [DllImport (_dllName)]
  private static extern double hv_getSampleRate(IntPtr ctx);

  [DllImport (_dllName)]
  private static extern int hv_getNumInputChannels(IntPtr ctx);

  [DllImport (_dllName)]
  private static extern int hv_getNumOutputChannels(IntPtr ctx);

  [DllImport (_dllName)]
  private static extern void hv_setSendHook(IntPtr ctx, SendHook sendHook);

  [DllImport (_dllName)]
  private static extern void hv_setPrintHook(IntPtr ctx, PrintHook printHook);

  [DllImport (_dllName)]
  private static extern int hv_setUserData(IntPtr ctx, IntPtr userData);

  [DllImport (_dllName)]
  private static extern IntPtr hv_getUserData(IntPtr ctx);

  [DllImport (_dllName)]
  private static extern void hv_sendBangToReceiver(IntPtr ctx, uint receiverHash);

  [DllImport (_dllName)]
  private static extern void hv_sendFloatToReceiver(IntPtr ctx, uint receiverHash, float x);

  [DllImport (_dllName)]
  private static extern uint hv_msg_getTimestamp(IntPtr message);

  [DllImport (_dllName)]
  private static extern bool hv_msg_hasFormat(IntPtr message, string format);

  [DllImport (_dllName)]
  private static extern float hv_msg_getFloat(IntPtr message, int index);

  [DllImport (_dllName)]
  private static extern bool hv_table_setLength(IntPtr ctx, uint tableHash, uint newSampleLength);

  [DllImport (_dllName)]
  private static extern IntPtr hv_table_getBuffer(IntPtr ctx, uint tableHash);

  [DllImport (_dllName)]
  private static extern float hv_samplesToMilliseconds(IntPtr ctx, uint numSamples);

  [DllImport (_dllName)]
  private static extern uint hv_stringToHash(string s);

  private delegate void PrintHook(IntPtr context, string printName, string str, IntPtr message);

  private delegate void SendHook(IntPtr context, string sendName, uint sendHash, IntPtr message);

  public Hv_osc_wavetable_single_Context(double sampleRate, int poolKb=10, int inQueueKb=2, int outQueueKb=2) {
    gch = GCHandle.Alloc(msgQueue);
    _context = hv_osc_wavetable_single_new_with_options(sampleRate, poolKb, inQueueKb, outQueueKb);
    hv_setPrintHook(_context, new PrintHook(OnPrint));
    hv_setUserData(_context, GCHandle.ToIntPtr(gch));
  }

  ~Hv_osc_wavetable_single_Context() {
    hv_delete(_context);
    GC.KeepAlive(_context);
    GC.KeepAlive(_sendHook);
    gch.Free();
  }

  public void RegisterSendHook() {
    // Note: send hook functionality only applies to messages containing a single float value
    if (_sendHook == null) {
      _sendHook = new SendHook(OnMessageSent);
      hv_setSendHook(_context, _sendHook);
    }
  }

  public bool IsSendHookRegistered() {
    return (_sendHook != null);
  }

  public double GetSampleRate() {
    return hv_getSampleRate(_context);
  }

  public int GetNumInputChannels() {
    return hv_getNumInputChannels(_context);
  }

  public int GetNumOutputChannels() {
    return hv_getNumOutputChannels(_context);
  }

  public void SendBangToReceiver(uint receiverHash) {
    hv_sendBangToReceiver(_context, receiverHash);
  }

  public void SendFloatToReceiver(uint receiverHash, float x) {
    hv_sendFloatToReceiver(_context, receiverHash, x);
  }

  public void FillTableWithFloatBuffer(string tableName, float[] buffer) {
    uint tableHash = hv_stringToHash(tableName);
    if (hv_table_getBuffer(_context, tableHash) != IntPtr.Zero) {
      hv_table_setLength(_context, tableHash, (uint) buffer.Length);
      Marshal.Copy(buffer, 0, hv_table_getBuffer(_context, tableHash), buffer.Length);
    } else {
      Debug.Log(string.Format("Table '{0}' doesn't exist in the patch context.", tableName));
    }
  }

  public uint StringToHash(string s) {
    return hv_stringToHash(s);
  }

  public int Process(float[] buffer, int numFrames) {
    return hv_processInlineInterleaved(_context, buffer, buffer, numFrames);
  }

  [MonoPInvokeCallback(typeof(PrintHook))]
  private static void OnPrint(IntPtr context, string printName, string str, IntPtr message) {
    float timeInSecs = hv_samplesToMilliseconds(context, hv_msg_getTimestamp(message)) / 1000.0f;
    Debug.Log(string.Format("{0} [{1:0.000}]: {2}", printName, timeInSecs, str));
  }

  [MonoPInvokeCallback(typeof(SendHook))]
  private static void OnMessageSent(IntPtr context, string sendName, uint sendHash, IntPtr message) {
    if (hv_msg_hasFormat(message, "f")) {
      SendMessageQueue msgQueue = (SendMessageQueue) GCHandle.FromIntPtr(hv_getUserData(context)).Target;
      msgQueue.AddMessage(sendName, hv_msg_getFloat(message, 0));
    }
  }
}
