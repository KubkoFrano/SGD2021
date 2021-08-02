using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] PowerUP = new GameObject[3];
    List<GameObject> ListOfPowerUPs = new List<GameObject>();
    HexagonGeneration hexGen;
    public int maxAmmountOfPowerUPs = 15;

    private void Start()
    {
        hexGen = GetComponent<HexagonGeneration>();

    }
    void Update()
    {
        if(Random.value < 0.5f)
        {
            if(ListOfPowerUPs.Count <= maxAmmountOfPowerUPs)
            {
                ListOfPowerUPs.Add(Instantiate(PowerUP[Random.Range(0, PowerUP.Length)]));
                int randomIndex = Random.Range(0, hexGen.HexagoneList.Count);
                ListOfPowerUPs[ListOfPowerUPs.Count - 1].transform.parent = hexGen.HexagoneList[randomIndex].transform.GetChild(6).transform;
                ListOfPowerUPs[ListOfPowerUPs.Count - 1].transform.position = hexGen.HexagoneList[randomIndex].transform.GetChild(6).transform.position;
                
            }
        }
    }
}
