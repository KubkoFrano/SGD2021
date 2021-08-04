using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveState : State
{
    public ReviveState(HexagonData data) : base(data)
    {
    }

    public override State Execute()
    {

        if (data.transform.position.y >= data.revivedHeight)
        {
            data.UpdateOnChange();
            return new BaseState(data);
        }
        else
        {
            data.acc += data.fallSpeed * Time.deltaTime;

            //data.Position = (Vector3.up * - data.acc);
            Vector3 newPos = data.transform.position + Vector3.up * data.acc;
            data.rb.MovePosition(newPos);

            return new ReviveState(data);
        }
    }
}
