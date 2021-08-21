using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonGeneration : MonoBehaviour
{
    [SerializeField]
    Vector2Int scale = new Vector2Int(3, 5);

    float hexagoneRadius = 5f;
    public float SpacingLenght = 0.5f;

    public GameObject[] Hexagons = new GameObject[3];
    public GameObject[] randomSpawnableHexagon = new GameObject[4];
    public GameObject[] randomSideHexagon = new GameObject[4];
    public GameObject[] middleHexagon = new GameObject[1];

    public List<GameObject> HexagoneList = new List<GameObject>();

    public Vector3[] spawnablePlayerPositions = new Vector3[4];

    public float offsetY = 0;

    public List<int> usedProps = new List<int>();

    int[] testSpawn = new int[] { -1, -1, -1, -1 };
    int[] testSide = new int[] { -1, -1, -1, -1 };

    int p = 0;
    int o = 0;

    

    void Awake()
    {
        GeneratePossitions();
        App.playerManager.SetSpawnPositions(spawnablePlayerPositions);
        App.playerManager.SetPlayerPositions();
    }

    public void GeneratePossitions()
    {
        float offsetX = 0; //offset for every row
        int addPart = -1;  //adding that extra part
        float positionOffset = SpacingLenght + 2 * hexagoneRadius;

        float sTest = 0;

        for (int y = 0, i = 0; y < scale.y; y++)
        {
            if (y <= (scale.y - 1) / 2)
            {
                offsetX -= positionOffset/2;
                addPart += 1;
            }
            else
            {
                offsetX += positionOffset/2;
                addPart -= 1;
            }

            
            for (int x = 0; x < scale.x + addPart; x++, i++) //just math stuff
            {
                

                int HexType = (int)Random.Range(0, Hexagons.Length);   //using random hexagon models
                Vector2 CalculatePos = new Vector2(x * positionOffset + offsetX, y * (positionOffset - 1));
                Vector3 nextPosition = new Vector3(CalculatePos.x, offsetY, CalculatePos.y);

                Hexagons[HexType].transform.position = nextPosition;
                HexagoneList.Add(Instantiate(Hexagons[HexType]));
                HexagoneList[i].transform.SetParent(this.transform);
                
                //HexagoneList[i].GetComponent<Renderer>().material = Hexagons[HexType].GetComponent<MeshRenderer>().sharedMaterial;

                
                if (isSpawnableCorner(i))
                {
                    
                    

                    bool again = true;
                    int randomI = 0;
                    while (again == true)
                    {
                        again = false;
                        randomI = Random.Range(0, (int)(randomSpawnableHexagon.Length));

                        for (int t = 0; t < randomSpawnableHexagon.Length; t++)
                        {
                            if (randomI == testSpawn[t]) again = true;
                        }

                    }
                    testSpawn[p] = randomI;

                    
                    HexagoneList[i].GetComponent<HexagonData>().spawningHexes = true;
                    HexagoneList[i].GetComponent<HexagonData>().SpawnProps = false;
                    GameObject SpawnProp = Instantiate(randomSpawnableHexagon[randomI]);
                    SpawnProp.transform.parent = HexagoneList[i].transform.GetChild(1);
                    SpawnProp.transform.position = HexagoneList[i].transform.GetChild(1).transform.position;
                    

                    HexagoneList[i].transform.Rotate(0, 60 * sTest, 0);   // 0, 5, 2, 3;

                    if (sTest == 0) sTest = 5;
                    else if (sTest == 5) sTest = 2;
                    else if (sTest == 2) sTest = 3;

                    p++;
                }
                else if (isSide(i))
                {
                    bool again = true;
                    int randomI = 0;
                    while (again == true)
                    {
                        again = false;
                        randomI = Random.Range(0, (int)(randomSideHexagon.Length));

                        for (int t = 0; t < randomSideHexagon.Length; t++)
                        {
                            if (randomI == testSide[t]) again = true;
                        }
                    }
                    testSide[o] = randomI;

                    HexagoneList[i].GetComponent<HexagonData>().SpawnProps = false;
                    HexagoneList[i].GetComponent<MeshRenderer>().material = randomSideHexagon[randomI].GetComponent<MeshRenderer>().sharedMaterial;


                    o++;
                }
                else if (isMiddle(i))
                {
                    int randomI = Random.Range(0, middleHexagon.Length);

                    HexagoneList[i].GetComponent<HexagonData>().SpawnProps = false;
                    GameObject SpawnProp = Instantiate(middleHexagon[randomI]);
                    SpawnProp.transform.parent = HexagoneList[i].transform.GetChild(1);
                    SpawnProp.transform.position = HexagoneList[i].transform.GetChild(1).transform.position;
                }
                
                
            }

        }

        updateSpawnablePositions();

        
    }

    public void Update()
    {
        //updateSpawnablePositions();
    }
    public void updateSpawnablePositions()
    {
        int oneIfEven = 0;
        if (scale.y % 2 == 0) oneIfEven = 1;

        spawnablePlayerPositions[0] = HexagoneList[0].transform.position;
        HexagoneList[0].GetComponent<HexagonData>().neverFalls = true;

        spawnablePlayerPositions[1] = HexagoneList[scale.x - 1].transform.position;
        HexagoneList[scale.x - 1].GetComponent<HexagonData>().neverFalls = true;

        spawnablePlayerPositions[2] = HexagoneList[HexagoneList.Count - scale.x + oneIfEven].transform.position;
        HexagoneList[HexagoneList.Count - scale.x + oneIfEven].GetComponent<HexagonData>().neverFalls = true;

        spawnablePlayerPositions[3] = HexagoneList[HexagoneList.Count - 1].transform.position;
        HexagoneList[HexagoneList.Count - 1].GetComponent<HexagonData>().neverFalls = true;

        for (int i = 0; i < spawnablePlayerPositions.Length; i++)
        {
            spawnablePlayerPositions[i] += Vector3.up * 15;
        }
    }

    bool isSpawnableCorner(int i)
    {
        //int lastIndex = HexagoneList.Count - 1;
        int addPart = 0;
        for (int n = 1; scale.y - n * 2 > 0; n++) addPart += scale.y - n * 2;
        int lastIndex = scale.x * scale.y - 1 + addPart;

        if (i == 0 || 
            i == lastIndex || 
            i == lastIndex - scale.x + 1 || 
            i == scale.x - 1) return true;


        return false;
    }

    bool isSide(int i)
    {
        int addPart = 0;
        for (int n = 1; scale.y - n * 2 > 0; n++) addPart += scale.y - n * 2;
        int lastIndex = scale.x * scale.y - 1 + addPart;
        int middleIndex = (lastIndex) / 2;

        if (i == (scale.x - 1) / 2 ||
            i == lastIndex - (scale.x - 1) / 2 ||
            i == middleIndex + (int)(((scale.x - 1) / 2) + ((scale.y - 1) / 2) * .5f) ||
            i == middleIndex - (int)(((scale.x - 1) / 2) + ((scale.y - 1) / 2) * .5f)) return true;
        return false;
    }

    bool isMiddle(int i)
    {
        int addPart = 0;
        for (int n = 1; scale.y - n * 2 > 0; n++) addPart += scale.y - n * 2;
        int lastIndex = scale.x * scale.y - 1 + addPart;
        int middleIndex = (lastIndex) / 2;
        if (i == middleIndex) return true;
        return false;
    }
}
