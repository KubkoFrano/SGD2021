using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomProps : MonoBehaviour
{
          
    public GameObject[] RandomDecorations = new GameObject[1];       //array of prop GameObjects
    Transform universalPropPos;

    void Start()
    {
        if (transform.GetComponent<HexagonData>().SpawnProps)
        {
            universalPropPos = transform.GetChild(1).GetComponent<Transform>();

            GameObject prop = Instantiate(RandomDecorations[Random.Range(0, RandomDecorations.Length)]);

            prop.transform.position = universalPropPos.position;

            prop.transform.SetParent(universalPropPos);
        }
    }
}
