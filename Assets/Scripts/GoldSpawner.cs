using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    public GameObject GoldPrefab;
    public List<GameObject> listOfGolds = new List<GameObject>();
    HexagonGeneration hexGen;
    public int maxAmmountOfGolds = 3;

    


    private void Start()
    {
        hexGen = GetComponent<HexagonGeneration>();
        

    }
    void Update()
    {

        if (listOfGolds.Count < maxAmmountOfGolds && Time.timeSinceLevelLoad > 2)
        {
            if (Random.value < 0.01f)
            {
                SpawnGoldsNeerMid();
            }
        }
    }

    void SpawnGoldsNeerMid()
    {
        Vector3 mid = new Vector3(GetComponent<HexagonOffset>().target.x, 10, GetComponent<HexagonOffset>().target.y);
        Collider[] middleHexagos = Physics.OverlapSphere(mid, 30, LayerMask.GetMask("Hexagon"));

        int randomIndex = Random.Range(0, middleHexagos.Length);

        if (middleHexagos[randomIndex].gameObject.transform.GetChild(0).childCount == 0)
        {

            listOfGolds.Add(Instantiate(GoldPrefab));

            listOfGolds[listOfGolds.Count - 1].transform.parent = middleHexagos[randomIndex].gameObject.transform.GetChild(0).transform;
            listOfGolds[listOfGolds.Count - 1].transform.position = middleHexagos[randomIndex].gameObject.transform.GetChild(0).transform.position;
            middleHexagos[randomIndex].gameObject.GetComponent<HexagonData>().neverFalls = true;
        }
    }


    public void RemoveGold(GameObject coin)
    {
        if (coin.transform.parent.childCount == 1)
        {
            coin.GetComponentInParent<HexagonData>().neverFalls = false;
            listOfGolds.Remove(coin.transform.parent.gameObject);
        }
    }
}
