using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourHexagon : MonoBehaviour
{
    public State state;
    

    private void Start()
    {
        state = new BaseState(GetComponent<HexagonData>());

        state.data.hexFallParticle.Stop();
        state.data.UpdateOnChange();
        
    }


    void FixedUpdate()
    {
        
        state = state.Execute();
        
    }

    public void Fall()
    {
        state.data.UpdateOnChange();
        state = new FallState(state.data);
    }

    public int CarrotSmash()
    {
        if (transform.GetChild(0).childCount == 1)
            if (transform.GetChild(0).GetChild(0).CompareTag("CoinMaster"))
            {
                int kids = transform.GetChild(0).GetChild(0).childCount;
                Destroy(transform.GetChild(0).GetChild(0).gameObject);
                return kids;
            }

        return 0;
    }

    public void MoveStateChange()
    {
        if (state.data.baseState)
        {
            state.data.UpdateOnChange();
            state = new MoveState(state.data);
        }
    }
}
