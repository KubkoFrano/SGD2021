using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonOffset : MonoBehaviour
{
    HexagonGeneration HexGenData;
    Vector3[] pos;
    public Vector2 target = new Vector2(0,0);
    float maxDistance;
    
    [SerializeField]
    float MaxHeightOffset = 20;

    
    private void Start()
    {
        
        HexGenData = this.GetComponent<HexagonGeneration>();
        
        pos = new Vector3[HexGenData.HexagoneList.Count];
        
        for (int i = 0; i < 4; i++)
        {
            target += new Vector2(HexGenData.spawnablePlayerPositions[i].x, HexGenData.spawnablePlayerPositions[i].z);
        }
        target /= 4;
        maxDistance = Vector2.Distance(target, new Vector2(HexGenData.spawnablePlayerPositions[0].x, HexGenData.spawnablePlayerPositions[0].z));


        RecalculateY();

        for(int i = 0; i < pos.Length; i++)
        {
            HexGenData.HexagoneList[i].transform.position = pos[i];
        }
        

    }

    public void UpdateTarget(Vector3 lowestPlayer)
    {
        target = new Vector2(lowestPlayer.x + Random.value * 20 - 10, lowestPlayer.z + Random.value * 20 - 10);
        RecalculateY();
    }

    void RecalculateY()
    {
        for (int i = 0; i < HexGenData.HexagoneList.Count; i++)
        {
            float dst = Vector2.Distance(new Vector2(HexGenData.HexagoneList[i].transform.position.x, HexGenData.HexagoneList[i].transform.position.z), target);
            pos[i] = new Vector3(HexGenData.HexagoneList[i].transform.position.x, (1 - dst / maxDistance) * MaxHeightOffset, HexGenData.HexagoneList[i].transform.position.z);
            HexGenData.HexagoneList[i].GetComponent<HexagonData>().offsetY = pos[i].y;
            HexGenData.HexagoneList[i].GetComponent<HexagonData>().replaceOffset = true;
        }
    }
    /*private void Update()
    {
        RecalculateY();
    }*/
}
