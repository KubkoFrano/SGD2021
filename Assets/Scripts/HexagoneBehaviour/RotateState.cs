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
        if (data.neverFalls == true) return new BaseState(data);
        
        //Quaternion target = Quaternion.Euler(0, 0, 0);
        data.transform.Rotate(0, data.direction * Time.deltaTime * 20, 0);

        if (data.transform.localEulerAngles.y >= data.lastRotationZ + data.rotateItteration * 60 || data.transform.localEulerAngles.y <= data.lastRotationZ - data.rotateItteration * 60)  // fakt dlha podmienka XD sry
        {
            if (data.replaceOffset == true) return new ReplaceOffsetState(data);
            else return new BaseState(data);
        }


        return new RotateState(data);
    }
}
