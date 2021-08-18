using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (listOfPowerUPs.Count < maxAmmountOfPowerUPs && Time.timeSinceLevelLoad > 2)
        {
            if (Random.value < 0.01f)
            {
                SpawnPowerUpNearPlayer(LowestPlayer());
            }
        }
    }

    Vector3 LowestPlayer()
    {
        Vector3 LowestPlayerPos = new Vector3(0, 1000, 0);

        for (int i = 0; i < playerTransform.Length; i++)
        {
            if (LowestPlayerPos.y > playerTransform[i].position.y) LowestPlayerPos = playerTransform[i].position;
        }

        return LowestPlayerPos;
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
                listOfPowerUPs.Add(Instantiate(PowerUP[Random.Range(0, PowerUP.Length)]));

                listOfPowerUPs[listOfPowerUPs.Count - 1].transform.parent = closeHexagons[randomIndex].gameObject.transform.GetChild(0).transform;
                listOfPowerUPs[listOfPowerUPs.Count - 1].transform.position = closeHexagons[randomIndex].gameObject.transform.GetChild(0).transform.position;
            }
        }

    }


    public void RemovePowerUp(GameObject powerUp)
    {
        listOfPowerUPs.Remove(powerUp);
    }
}
