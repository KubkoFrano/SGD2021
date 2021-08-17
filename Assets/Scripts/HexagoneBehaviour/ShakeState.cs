using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeState : State
{
    public ShakeState(HexagonData data, bool shakeMultiplePlatforms) : base(data)
    {
        data.shakeMultiplePlatforms = shakeMultiplePlatforms;
    }
    public override State Execute()
    {
        data.spawnGolds = false;
        if (data.neverFalls == true)
        {
            return new BaseState(data);
        }
        data.timeSinceLastChange += Time.deltaTime;
        data.transform.position =  data.lastPosition + new Vector3(Random.value * .5f, 0, Random.value * .5f);

        if (data.timeSinceLastChange > data.shakeDuration)
        {
            if(data.shakeMultiplePlatforms == true)
            {
                data.activateNeighbours();
                
                //data.shakeMultiplePlatforms = false;
            }

            //data.UpdateOnChange();
            data.hexFallParticle.Play();
            return new FallState(data);
        }





        return new ShakeState(data, data.shakeMultiplePlatforms);
    }
}
