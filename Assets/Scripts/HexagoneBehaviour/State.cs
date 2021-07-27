using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public HexagonData data;
    
    static public bool isInterupted = false;

    public State(HexagonData data)
    {
        this.data = data;
        
    }

    static public void SetInterupt(bool interupt)
    {
        isInterupted = interupt;
    }

    public abstract State Execute();
}
