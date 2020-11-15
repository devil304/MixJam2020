using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class ControlSaturationContrast : MonoBehaviour
{
    [SerializeField] VolumeProfile VP;
    ColorAdjustments ca;
    [SerializeField] float maxSat=100f, maxCon=100f;
    // Start is called before the first frame update
    void Start()
    {
        VP.TryGet(out ca);
    }

    public void AdjSat(float val)
    {
        ca.saturation.value = val + ca.saturation.value>maxSat?maxSat: val + ca.saturation.value;
    }

    public void AdjCon(float val)
    {
        ca.contrast.value += val + ca.contrast.value > maxCon ? maxCon : val + ca.contrast.value;
    }
}
