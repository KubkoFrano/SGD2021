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

    int[] testSpawn = new int[] { -1, -1, -1, -1 };
    int[] testSide = new int[] { -1, -1, -1, -1 };

    int sp = 0;
    int si = 0;
    int p = 0;
    int o = 0;

    void Start()
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

                Hexagons[0].transform.position = nextPosition;
                HexagoneList.Add(Instantiate(Hexagons[0]));
                HexagoneList[i].transform.SetParent(this.transform);

                HexagoneList[i].GetComponent<MeshFilter>().mesh = Hexagons[HexType].GetComponent<MeshFilter>().sharedMesh;

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

                    HexagoneList[i].GetComponent<HexagonData>().SpawnProps = false;
                    HexagoneList[i].GetComponent<MeshFilter>().mesh = randomSpawnableHexagon[randomI].GetComponent<MeshFilter>().sharedMesh;
                    HexagoneList[i].GetComponent<MeshRenderer>().material = randomSpawnableHexagon[randomI].GetComponent<MeshRenderer>().sharedMaterial;

                    /*randomSpawnableHexagon[randomI].transform.position = nextPosition;
                    HexagoneList.Add(Instantiate(randomSpawnableHexagon[randomI]));*/

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
                    HexagoneList[i].GetComponent<MeshFilter>().mesh = randomSideHexagon[randomI].GetComponent<MeshFilter>().sharedMesh;
                    HexagoneList[i].GetComponent<MeshRenderer>().material = randomSideHexagon[randomI].GetComponent<MeshRenderer>().sharedMaterial;

                    /*randomSideHexagon[randomI].transform.position = nextPosition;
                    HexagoneList.Add(Instantiate(randomSideHexagon[randomI]));*/

                    o++;
                }
                else if (isMiddle(i))
                {
                    int randomI = Random.Range(0, middleHexagon.Length);

                    HexagoneList[i].GetComponent<HexagonData>().SpawnProps = false;
                    HexagoneList[i].GetComponent<MeshFilter>().mesh = middleHexagon[randomI].GetComponent<MeshFilter>().sharedMesh;
                    HexagoneList[i].GetComponent<MeshRenderer>().material = middleHexagon[randomI].GetComponent<MeshRenderer>().sharedMaterial;

                    /*middleHexagon[randomI].transform.position = nextPosition;
                    HexagoneList.Add(Instantiate(middleHexagon[randomI]));*/
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
