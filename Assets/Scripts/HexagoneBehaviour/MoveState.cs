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

        if      (data.transform.position.y > data.offsetY + data.magnitude * .6f) data.direction = -1;
        else if (data.transform.position.y < data.offsetY - data.magnitude * .6f) data.direction = 1;

        if (data.smoothStart >= Random.Range(1f, 2.5f))
        {
            data.UpdateOnChange();
            return new BaseState(data);
        }

        Vector3 move = new Vector3(data.spawnedPosition.x, data.lastPosition.y + data.direction * data.randomOff * Time.deltaTime * 4.0f, data.spawnedPosition.z);
        data.rb.MovePosition(move);
        data.lastPosition = data.transform.position;

        return new MoveState(data);
    }

}
