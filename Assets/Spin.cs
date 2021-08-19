using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{

    void Update()
    {
        this.transform.Translate(0, (Mathf.Sin(Time.time * 3f)/ 4) * Time.deltaTime, 0);
        this.transform.Rotate(0, 0.5f, 0);
    }
}
