using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReplaceOffsetState : State
{
    public ReplaceOffsetState(HexagonData data) : base(data)
    {
    }

    public override State Execute()
    {
        if (data.smoothStart < 1) data.smoothStart += 0.02f;
        else data.smoothStart = 1;

        Vector3 newPos = new Vector3(data.spawnedPosition.x, data.lastPosition.y * (1 - data.smoothStart) + data.offsetY * (data.smoothStart), data.spawnedPosition.z);

        data.rb.MovePosition(newPos);

        if(data.transform.position.y == data.offsetY)
        {
            data.UpdateOnChange();
            return new BaseState(data);
        }


        return new ReplaceOffsetState(data);
    }

    
    
}
