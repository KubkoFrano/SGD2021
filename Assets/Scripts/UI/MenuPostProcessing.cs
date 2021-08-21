using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MenuPostProcessing : MonoBehaviour
{
    Volume volume;

    private void Awake()
    {
        App.menuPostProcessing = this;
        volume = GetComponent<Volume>();
    }

    public void SwitchPostProcessing(bool value)
    {
        volume.enabled = value;
    }
}