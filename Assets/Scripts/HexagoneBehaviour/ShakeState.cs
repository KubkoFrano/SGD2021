using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeState : State
{
    public ShakeState(HexagonData data) : base(data)
    {

    }
    public override State Execute()
    {

        data.timeSinceLastChange += Time.deltaTime;
        data.Position =  data.lastPosition + new Vector3(Random.value * .5f, 0, Random.value * .5f);

        if (data.timeSinceLastChange > data.shakeDuration)
        {
            data.UpdateOnChange();
            return new FallState(data);
        }





        return new ShakeState(data);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
