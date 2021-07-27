using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitateState : State
{
    
    public LevitateState(HexagonData data) : base(data)
    {
    }
    public override State Execute()
    {
        if (data.smoothStart < 1) data.smoothStart += 0.001f;
        else data.smoothStart = 1;

        float y = PerlinNoiseMove(data.spawnedPosition.x, data.spawnedPosition.z) * data.smoothStart;
        data.Position = new Vector3(data.lastPosition.x, (data.lastPosition.y * (1 - data.smoothStart)) + y * data.magnitude, data.lastPosition.z);

        data.timeSinceLastChange += Time.deltaTime;
        if (data.timeSinceLastChange > 8f)
        {
            data.UpdateOnChange();
            return new BaseState(data);
        }

        Debug.Log("LevitateState");

        return new LevitateState(data);
    }

    float PerlinNoiseMove(float x, float y)
    {
        x += Time.time * data.elevateSpeed;
        y += Time.time * data.elevateSpeed;
        return (Mathf.PerlinNoise(x, y) - .5f) * 2;
    }
}
