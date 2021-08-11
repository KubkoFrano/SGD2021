using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourHexagon : MonoBehaviour
{
    public State state;
    

    private void Start()
    {
        state = new BaseState(GetComponent<HexagonData>());
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
        Debug.Log("fall");
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
