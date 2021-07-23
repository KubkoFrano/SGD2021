using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonBehavior : MonoBehaviour
{
    enum States
    {
        BaseState, ElevateState, ShakeState
    }
    States state;
    float elevateStateTime;

    private void Start()
    {
        elevateStateTime = Random.Range(0, 60);
        state = States.BaseState;
    }


    void Update()
    {
        switch (state)
        {
            case States.BaseState:
                BaseState();
                break;

            case States.ElevateState:
                ElevateState();
                break;

            case States.ShakeState:
                ShakeState();
                break;
        }
    }

    float stationaryTime = 0;


    void BaseState()                                        // Do basic Stationary State
    {



        stationaryTime += Time.deltaTime;


        if (stationaryTime > elevateStateTime)
        {
            state = States.ElevateState;
        }
        else if (Random.Range(0, 500) < 0)  //Change it later!!!
        {
            state = States.ShakeState;
        }

    }

    void ElevateState()                             // Do elevating State
    {

        float y = PerlinNoiseMove(this.transform.position.x, this.transform.position.z);
        this.transform.position = new Vector3(this.transform.position.x, y, this.transform.position.z);
    }

    void ShakeState()
    {
        //Do ShakeState script/code please XD
    }


    float PerlinNoiseMove(float x, float y)
    {
        x += Time.time * 4;
        y += Time.time * 4;
        return Mathf.PerlinNoise(x, y);
    }
}
