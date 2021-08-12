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

        data.smoothStart += 0.01f;

        if      (data.transform.position.y > data.offsetY + data.magnitude) data.direction = -1;
        else if (data.transform.position.y < data.offsetY - data.magnitude) data.direction = 1;

        if (data.smoothStart >= Random.Range(1f, 2.5f))
        {
            data.UpdateOnChange();
            return new BaseState(data);
        }
        /*
        Vector3 move = new Vector3(data.spawnedPosition.x, data.lastPosition.y + data.direction * data.randomOff * Time.deltaTime * 7f, data.spawnedPosition.z);
        data.rb.MovePosition(move);
        data.lastPosition = move;
        */

        Vector3 newPos = new Vector3(data.spawnedPosition.x, data.transform.position.y, data.spawnedPosition.z) + Vector3.up * .05f * data.direction;
        data.rb.MovePosition(newPos);

        return new MoveState(data);
    }

}
