using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField]
    float spid = 1f;
    [SerializeField]
    float magnitude = 1f;

    void Update()
    {
        this.transform.Translate(0, (Mathf.Sin(Time.time * spid) * magnitude) * Time.deltaTime, 0);
        this.transform.Rotate(0, 0.5f, 0);
    }
}
