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
        if (data.transform.position.y <= -10)
        {

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

            //data.Position = (Vector3.up * - data.acc);
            Vector3 newPos = data.transform.position + Vector3.up * data.acc;
            data.rb.MovePosition(newPos);

        }


        




        return new FallState(data);
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
