using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] Material[] materials;

    [SerializeField] SkinnedMeshRenderer meshRenderer;

    public void SetCharacter(int index)
    {
        meshRenderer.material = materials[index];
    }
}