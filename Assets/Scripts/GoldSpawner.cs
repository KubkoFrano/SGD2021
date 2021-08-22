using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    public GameObject GoldPrefab;
    public List<GameObject> listOfGolds = new List<GameObject>();
    HexagonGeneration hexGen;
    public int maxAmmountOfGoldMasters = 3;
    


    private void Start()
    {
        hexGen = GetComponent<HexagonGeneration>();

    }
    void Update()
    {
        GameObject []coins = GameObject.FindGameObjectsWithTag("CoinMaster");


        
        if (coins.Length < maxAmmountOfGoldMasters && Time.timeSinceLevelLoad > 2)
        {
            if (Random.value < 0.01f || coins.Length == 0)
            {
                SpawnGoldsNeerMid();
            }
        }

        foreach(GameObject i in coins)
        {
            if (i.transform.childCount == 0) Destroy(i);
        }
    }

    void SpawnGoldsNeerMid()
    {
        List<GameObject> hex = hexGen.HexagoneList;

        float[] topHeight = new float[5];
        GameObject[] topHexagons = new GameObject[5];

        foreach (GameObject i in hex)
        {
            for (int x = 0; x < topHeight.Length; x++)
            {
                if (i.transform.position.y > topHeight[x])
                {
                    if (i.GetComponent<HexagonData>().spawnGolds)
                    {
                        topHeight[x] = i.transform.position.y;
                        topHexagons[x] = i;
                        break;
                    }
                }
            }
        }

        int randomIndex = Random.Range(0, topHexagons.Length);

        if (topHexagons[randomIndex].gameObject.transform.GetChild(0).childCount == 0)
        {

            listOfGolds.Add(Instantiate(GoldPrefab));

            listOfGolds[listOfGolds.Count - 1].transform.parent = topHexagons[randomIndex].gameObject.transform.GetChild(0).transform;
            listOfGolds[listOfGolds.Count - 1].transform.position = topHexagons[randomIndex].gameObject.transform.GetChild(0).transform.position;
            //topHexagons[randomIndex].gameObject.GetComponent<HexagonData>().neverFalls = true;
        }
    }


    public void RemoveGold(GameObject coin)
    {
        if (coin.transform.parent.childCount == 1)
        {
            listOfGolds.Remove(coin.transform.parent.gameObject);
        }
    }
}
