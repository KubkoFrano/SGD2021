using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateState : State
{
    public RotateState(HexagonData data) : base(data)
    {
    }

    public override State Execute()
    {
        Quaternion target = Quaternion.Euler(0, 0, 0);
        data.transform.Rotate(0, 0, data.direction * Time.deltaTime * 20);

        if (data.transform.localEulerAngles.y >= data.lastRotationZ + data.rotateItteration * 60 || data.transform.localEulerAngles.y <= data.lastRotationZ - data.rotateItteration * 60)  // fakt dlha podmienka XD sry
        {
            return new BaseState(data);
        }


        return new RotateState(data);
    }
}