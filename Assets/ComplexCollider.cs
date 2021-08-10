using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexCollider : MonoBehaviour
{
    
    void Start()
    {
        Mesh newMesh = GetComponent<MeshFilter>().mesh;
        MeshCollider collider = gameObject.AddComponent<MeshCollider>();
        collider.sharedMesh = newMesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
