using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncFallState : MonoBehaviour
{
    public Vector2 targetPlatform = new Vector2(8, 8);
    int index = 0;

    HexagonGeneration HexGen;
    BehaviourHexagon behaviour;


    void Start()
    {
        StartCoroutine(LateStart(5));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ShakeCentralDown();
    }
    public void ShakeCentralDown()
    {
        HexGen = GetComponent<HexagonGeneration>();
        float minDistance = 25f;

        
        foreach (GameObject i in HexGen.HexagoneList)
        {
            if (Vector2.Distance(new Vector2(i.transform.position.x, i.transform.position.z), targetPlatform) < minDistance)
            {
                minDistance = Vector2.Distance(new Vector2(i.transform.position.x, i.transform.position.z), targetPlatform);
                index = HexGen.HexagoneList.IndexOf(i);
            }
        }

        behaviour = this.transform.GetChild(HexGen.HexagoneList[index].transform.GetSiblingIndex()).GetComponent<BehaviourHexagon>();

        behaviour.state.data.UpdateOnChange();
        behaviour.state = new ShakeState(behaviour.state.data, true);

    }
    


    public void ShakeThemDown()
    {
        foreach (GameObject i in HexGen.HexagoneList)
        {
            if (Vector2.Distance(new Vector2(i.transform.position.x,                          i.transform.position.z), 
                                 new Vector2(HexGen.HexagoneList[index].transform.position.x, HexGen.HexagoneList[index].transform.position.z)) < 10)
            {
                //HexGen.HexagoneList.IndexOf(i);

                behaviour = this.transform.GetChild(i.transform.GetSiblingIndex()).GetComponent<BehaviourHexagon>();

                behaviour.state.data.UpdateOnChange();
                behaviour.state = new ShakeState(behaviour.state.data, false);
            }
        }

        
    }



    

}
