using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomProps : MonoBehaviour
{
          
    public GameObject[] RandomDecorations = new GameObject[1];       //array of prop GameObjects
    Transform universalPropPos;

    void Start()
    {
        //if (Random.value < .2f) 
        {
            if (transform.GetComponent<HexagonData>().SpawnProps)
            {
                universalPropPos = transform.GetChild(1).GetComponent<Transform>();
                int randomIndex = Random.Range(0, RandomDecorations.Length);

                GameObject prop = Instantiate(RandomDecorations[randomIndex]);

                prop.transform.Rotate(0, Random.Range(0, 6) * 60, 0);
                prop.transform.position = universalPropPos.position;

                prop.transform.SetParent(universalPropPos);

                /*
                bool again = true;
                while (again)
                {
                    universalPropPos = transform.GetChild(1).GetComponent<Transform>();

                    int randomIndex = Random.Range(0, RandomDecorations.Length);

                    if (GetComponentInParent<HexagonGeneration>().usedProps.Count >= RandomDecorations.Length)
                    {
                        again = false;
                    }
                    else if (!GetComponentInParent<HexagonGeneration>().usedProps.Contains(randomIndex))
                    {
                        GetComponentInParent<HexagonGeneration>().usedProps.Add(randomIndex);
                        GameObject prop = Instantiate(RandomDecorations[randomIndex]);

                        prop.transform.Rotate(0, Random.Range(0, 360), 0);
                        prop.transform.position = universalPropPos.position;

                        prop.transform.SetParent(universalPropPos);

                        again = false;
                    }
                    
                }
                */


            }
        }  
       
    }
}
