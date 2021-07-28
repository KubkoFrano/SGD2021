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


    void Update()
    {
        if (State.isInterupted)
        {
            State.SetInterupt(false);
            state = new FallState(state.data);
            
        }


        state = state.Execute();
        
    }
}
