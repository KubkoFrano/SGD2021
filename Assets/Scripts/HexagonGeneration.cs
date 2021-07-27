using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonGeneration : MonoBehaviour
{
    [SerializeField]
    int maxWidth = 5;
    [SerializeField]
    int maxHeight = 5;

    float hexagoneRadius = 3.5f;
    public float SpacingLenght = 0.5f;

    public GameObject[] Hexagons = new GameObject[3];
    

    void Start()
    {
        GeneratePossitions();
    }

    public void GeneratePossitions()
    {
        float offsetX = 0; //offset for every row
        int addPart = -1;  //adding that extra part
        float positionOffset = SpacingLenght + 2 * hexagoneRadius;

        for (int y = 0; y < maxWidth; y++)
        {
            if (y <= (maxWidth - 1) / 2)
            {
                offsetX -= positionOffset/2;
                addPart += 1;
            }
            else
            {
                offsetX += positionOffset/2;
                addPart -= 1;
            }

            for (int x = 0; x < maxHeight + addPart; x++) //just math stuff
            {


                int HexType = (int)Random.Range(0, Hexagons.Length);   //using random hexagon models
                Vector2 CalculatePos = new Vector2(x * positionOffset + offsetX, y * positionOffset );
                Vector3 nextPosition = new Vector3(CalculatePos.x, Mathf.PerlinNoise(CalculatePos.x/16, CalculatePos.y/16) * 6 - 3, CalculatePos.y);

                Hexagons[HexType].transform.position = nextPosition;

                GameObject hexagone = Instantiate(Hexagons[HexType]);

                hexagone.transform.SetParent(this.transform);
            }

        }
    }
}
