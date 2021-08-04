using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : State
{
    public BaseState(HexagonData data) : base(data)
    {
    }

    public override State Execute()
    { 


        data.timeSinceLastChange += Time.deltaTime;

        if (data.replaceOffset == true)
        {
            data.UpdateOnChange();
            return new ReplaceOffsetState(data);
        }
        if (data.timeSinceLastChange > data.elevateStateTime)       //ChangeItLater!!!
        {
            data.UpdateOnChange();
            return new LevitateState(data);
        }
        else if (Random.value < 0.0002f)         //Change it later!!!
        {
            data.UpdateOnChange();
            return new ShakeState(data, false);
        }
        else if (Random.value < 0.0002f)
        {
            data.UpdateOnChange();
            return new RotateState(data);
        }
        


            return new BaseState(data);
    }
}
