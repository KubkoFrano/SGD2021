using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonGeneration : MonoBehaviour
{
    public int maxWidth = 5;
    public int maxHeight = 5;

    public GameObject[] Hexagons = new GameObject[3];

    void Start()
    {
        GeneratePossitions();
    }

    public void GeneratePossitions()
    {
        float offsetX = 0; //offset for every row
        int addPart = -1;  //adding that extra part

        for (int y = 0; y < maxWidth; y++)
        {
            if (y <= (maxWidth - 1) / 2)
            {
                offsetX -= 4f;
                addPart += 1;
            }
            else
            {
                offsetX += 4f;
                addPart -= 1;
            }

            for (int x = 0; x < maxHeight + addPart; x++) //just math stuff
            {


                int HexType = (int)Random.Range(0, Hexagons.Length);   //using random hexagon models

                Vector3 nextPosition = new Vector3(x * 8 + offsetX, 0, y * 7);

                Hexagons[HexType].transform.position = nextPosition;

                GameObject hexagone = Instantiate(Hexagons[HexType]);

                hexagone.transform.SetParent(this.transform);
            }

        }
    }
}
