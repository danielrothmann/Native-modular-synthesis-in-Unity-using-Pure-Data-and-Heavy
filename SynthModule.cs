using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SynthModule : MonoBehaviour {

    public enum ModuleType { Source, Effect, Controller }
    public ModuleType Type { get; protected set; }

    public abstract void ProcessBuffer(float[] buffer, int numChannels);

    public abstract void SetParameter(int paramIndex, float value);
    public abstract float GetParameter(int paramIndex);
}
