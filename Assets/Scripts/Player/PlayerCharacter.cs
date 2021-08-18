using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] Material[] materials;

    SkinnedMeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void SetCharacter(int index)
    {
        meshRenderer.material = materials[index];
    }
}