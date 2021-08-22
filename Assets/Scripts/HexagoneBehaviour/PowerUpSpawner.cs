using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] PowerUP = new GameObject[3];
    public List<GameObject> listOfPowerUPs = new List<GameObject>();
    HexagonGeneration hexGen;
    public int maxAmmountOfPowerUPs = 15;

    Transform[] playerTransform;


    private void Start()
    {
        hexGen = GetComponent<HexagonGeneration>();
        playerTransform = App.playerManager.GetPlayerTransforms();

    }
    void Update()
    {

        if (listOfPowerUPs.Count < maxAmmountOfPowerUPs)
        {
            if (Random.value < 0.02f)
            {
                SpawnPowerUpNearPlayer(LowestPlayer());
            }
        }
    }

    Vector3 LowestPlayer()
    {
        PlayerScore[] scores = App.playerManager.GetPlayerScores();
        Vector3 target;

        Array.Sort(scores, new Comparison<PlayerScore>(
                  (i1, i2) => i1.GetScore().CompareTo(i2.GetScore())));
        

        if      (Random.value < .5 * (4 / scores.Length)) target = new Vector3(scores[0].transform.position.x, 0, scores[0].transform.position.z);
        else if (Random.value < .8 * (4 / scores.Length)) target = new Vector3(scores[1].transform.position.x, 0,scores[1].transform.position.z);
        else                                              target = new Vector3(scores[2].transform.position.x, 0,scores[2].transform.position.z);



        return target;
    }

    void SpawnPowerUpNearPlayer(Vector3 lowestPlayer)
    {
        Collider[] closeHexagons = Physics.OverlapSphere(lowestPlayer, 16, LayerMask.GetMask("Hexagon"));
        Collider[] tooClose = Physics.OverlapSphere(lowestPlayer, 5, LayerMask.GetMask("Hexagon"));


        List<int> listOfRandomIndexes = new List<int>();

        for (int i = 0; i < closeHexagons.Length; i++)
        {
            bool isntRepeating = true;
            for (int p = 0; p < tooClose.Length; p++)
            {
                if (closeHexagons[i] == tooClose[p]) isntRepeating = false;
            }
            if (isntRepeating)
                listOfRandomIndexes.Add(i);
        }




        if (listOfRandomIndexes.Count != 0)
        {
            int randomIndex = listOfRandomIndexes[Random.Range(0, listOfRandomIndexes.Count)];

            if (closeHexagons[randomIndex].gameObject.transform.GetChild(0).childCount == 0)
            {
                if (!closeHexagons[randomIndex].GetComponent<HexagonData>().neverFalls || closeHexagons[randomIndex].GetComponent<HexagonData>().SpawnProps)
                {
                    listOfPowerUPs.Add(Instantiate(PowerUP[Random.Range(0, PowerUP.Length)]));

                    listOfPowerUPs[listOfPowerUPs.Count - 1].transform.parent = closeHexagons[randomIndex].gameObject.transform.GetChild(0).transform;
                    listOfPowerUPs[listOfPowerUPs.Count - 1].transform.position = closeHexagons[randomIndex].gameObject.transform.GetChild(0).transform.position;
                }
            }
        }

    }


    public void RemovePowerUp(GameObject powerUp)
    {
        listOfPowerUPs.Remove(powerUp);
    }
}
