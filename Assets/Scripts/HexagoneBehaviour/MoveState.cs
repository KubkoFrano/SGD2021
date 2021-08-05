using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public MoveState(HexagonData data) : base(data)
    {
    }

    public override State Execute()
    {

        if (data.smoothStart < 1.5f) data.smoothStart += 0.01f;
        //else data.smoothStart = 1.5f;


        if (data.transform.position.y >= data.offsetY + data.magnitude / 2 || data.transform.position.y <= data.offsetY - data.magnitude / 2)
        {
            data.UpdateOnChange();
            new BaseState(data);
        }
        Vector3 move = new Vector3(data.spawnedPosition.x, data.lastPosition.y + data.direction * data.randomOff * data.magnitude/2 * data.smoothStart, data.spawnedPosition.z);
        data.rb.MovePosition(move);

        return new MoveState(data);
    }

}
