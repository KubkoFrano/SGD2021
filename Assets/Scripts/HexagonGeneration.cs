using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonGeneration : MonoBehaviour
{
    public int maxWidth = 5;
    public int maxHeight = 5;

    public int index = 0;

    public List<Vector2Int> position = new List<Vector2Int>();

    float hexagoneRadius = 3.5f;
    public float SpacingLenght = 0.5f;

    public GameObject[] Hexagons = new GameObject[3];
    public List<GameObject> HexagoneList = new List<GameObject>();

    public Vector3[] SpawnablePlayerPositions = new Vector3[4];

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

                HexagoneList.Add(Instantiate(Hexagons[HexType]));
                HexagoneList[index].transform.SetParent(this.transform);

                index++;
                position.Add(new Vector2Int(x, y));
            }

        }
        int oneIfEven = 0;
        if (maxHeight % 2 == 0) oneIfEven = 1;
        
        SpawnablePlayerPositions[0] = HexagoneList[0].transform.position;
        SpawnablePlayerPositions[1] = HexagoneList[maxHeight - 1].transform.position;
        SpawnablePlayerPositions[2] = HexagoneList[HexagoneList.Count - maxHeight + oneIfEven].transform.position;
        SpawnablePlayerPositions[3] = HexagoneList[HexagoneList.Count - 1].transform.position;

        for(int i = 0; i < SpawnablePlayerPositions.Length; i ++)
        {
            SpawnablePlayerPositions[i] += Vector3.up * 5;
        }
    }
}
