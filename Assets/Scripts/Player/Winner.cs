using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer mesh;

    [SerializeField] Material[] materials;

    HatManager hatManager;

    private void Awake()
    {
        hatManager = GetComponentInChildren<HatManager>();
    }

    private void Start()
    {
        int index = App.playerManager.GetBestPlayerIndex();

        mesh.material = materials[index];
        hatManager.EnableHat(index);
    }
}