using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncFallState : MonoBehaviour
{
    public Vector2 targetPlatform = new Vector2(8, 8);
    int index = 0;

    HexagonGeneration HexGen;
    BehaviourHexagon behaviour;

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
        int randomAmmount = Random.Range(1, 5);

        int n = 0;
        foreach (GameObject i in HexGen.HexagoneList)
        {
            if (Vector2.Distance(new Vector2(i.transform.position.x,                          i.transform.position.z), 
                                 new Vector2(HexGen.HexagoneList[index].transform.position.x, HexGen.HexagoneList[index].transform.position.z)) < 16)
            {
                //if (n < randomAmmount && Random.value < .7f)
                
                    behaviour = this.transform.GetChild(i.transform.GetSiblingIndex()).GetComponent<BehaviourHexagon>();

                    behaviour.state.data.UpdateOnChange();
                    behaviour.state = new ShakeState(behaviour.state.data, false);
                    n++;
                
            }
        }

        
    }

    /*
    public void MoveStateChange()
    {
        foreach (GameObject i in HexGen.HexagoneList)
        {
            behaviour = this.transform.GetChild(i.transform.GetSiblingIndex()).GetComponent<BehaviourHexagon>();

            behaviour.state.data.UpdateOnChange();
            behaviour.state = new ShakeState(behaviour.state.data, false);
        }
    }
    */




}
