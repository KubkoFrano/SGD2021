using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class InGamePostProcessing : MonoBehaviour
{
    [SerializeField] Volume volume;
    DepthOfField depth;

    float blurred = 0.1f;
    float normal = 6.08f;

    private void Awake()
    {
        App.inGamePostProcessing = this;
    }

    private void Start()
    {
        volume.profile.TryGet<DepthOfField>(out depth);
    }

    public void Blur()
    {
        depth.focusDistance.value = blurred;
    }

    public void Unblur()
    {
        depth.focusDistance.value = normal;
    }
}