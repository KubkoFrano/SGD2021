using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : State
{
    public FallState(HexagonData data) : base(data)
    {
    }
    public override State Execute()
    {
        data.spawnGolds = false;

        if (data.transform.position.y <= -25)
        {
            data.hexFallParticle.Stop();
            data.timeSinceLastChange += Time.deltaTime;
            if (data.timeSinceLastChange > 10f)
            {
                data.UpdateOnChange();


                return new ReviveState(data);
            }
        }
        else
        {
            data.acc -= data.fallSpeed * Time.deltaTime;

            Vector3 newPos = new Vector3(data.spawnedPosition.x, data.transform.position.y, data.spawnedPosition.z) + Vector3.up * data.acc;
            data.rb.MovePosition(newPos);

        }

        return new FallState(data);
    }
}
