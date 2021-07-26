using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonBehavior : MonoBehaviour
{

    [SerializeField]
    float elevatingSpeed = 2f;
    [SerializeField]
    float elevatingMagnitude = 3f;
    [SerializeField]
    float shakeDuration = 3f;
    [SerializeField]
    float fallingSpeed = 1f;
    

    
    enum States
    {
        BaseState,
        LevitateState,
        ShakeState,
        FallState,
        ReviveState,
        ElevateState
    }
    States state;
    float elevateStateTime;
    Vector3 spawnedPossition;

    private void Start()
    {
        elevateStateTime = Random.Range(0, 80);
        state = States.BaseState;
        spawnedPossition = this.transform.position;
    }


    void Update()
    {
        switch (state)
        {
            case States.BaseState:
                BaseState();
                break;

            case States.LevitateState:
                LevitateState();
                break;

            case States.ShakeState:
                ShakeState();
                break;
            case States.FallState:
                FallState();
                break;
            case States.ReviveState:
                ReviveState();
                break;
            case States.ElevateState:
                ElevateState();
                break;
        }
    }

    float stationaryTime = 0;


    void BaseState()                                        // Do basic Stationary State
    {



        stationaryTime += Time.deltaTime;


        if (stationaryTime > elevateStateTime)
        {
            smoothStart = 0; 
            stationaryTime = 0;
            lastPos = this.transform.position;
            state = States.LevitateState;
        }
        else if (Random.value < 0.0002f)         //Change it later!!!
        {
            lastPos = this.transform.position;
            state = States.ShakeState;
        }
        else if (Random.value < 0.002f)         //Change it later!!!
        {
            state = States.ElevateState;
        }

    }

    float smoothStart = 0;
    float elevatingDuration = 0;
    
    Vector3 lastPos;
    void LevitateState()                                 // Do elevating State
    {
        if (smoothStart < 1) smoothStart += 0.001f;
        else smoothStart = 1;

        float y = PerlinNoiseMove(this.transform.position.x, this.transform.position.z) * smoothStart;
        this.transform.position = new Vector3(this.transform.position.x, (lastPos.y * (1 - smoothStart)) + y * elevatingMagnitude, this.transform.position.z);

        elevatingDuration += Time.deltaTime;
        if (elevatingDuration > 8f)
        {
            elevatingDuration = 0;
            state = States.BaseState;
        }
    }
    
    float shakeStateTimer = 0;
    void ShakeState()
    {
        shakeStateTimer += Time.deltaTime;
        this.transform.position = lastPos + new Vector3(Random.Range(0f, .5f), 0, Random.Range(0f, .5f));
        
        if (shakeStateTimer > shakeDuration) 
        { 
            state = States.FallState;    // switch to fallState
            shakeStateTimer = 0;
        }
    }

    float acceleration = 0;
    float reviveDelay = 0;
    void FallState()
    {
        if (this.transform.position.y <= -10)
        {
            acceleration = 0;
            reviveDelay += Time.deltaTime;
            if (reviveDelay > 10f)
            {
                reviveDelay = 0;
                state = States.ReviveState;
            }
        }
        else acceleration += fallingSpeed * Time.deltaTime;
           

        this.transform.Translate(Vector3.forward * -acceleration);
    }

    void ReviveState()
    {
        if (this.transform.position.y >= Random.Range(-elevatingMagnitude, +elevatingMagnitude))
        {
            acceleration = 0;
            state = States.BaseState;
        }
        else acceleration += fallingSpeed * Time.deltaTime;

        this.transform.Translate(Vector3.forward * acceleration);
    }

    void ElevateState()
    {

        if (Random.value > .5f) // elevate upp
        {
            acceleration += (fallingSpeed / 20) * Time.deltaTime;
            this.transform.Translate(Vector3.forward * acceleration);
            if (this.transform.position.y >= elevatingMagnitude / 4 + elevatingMagnitude /2 * Random.value)
            {
                acceleration = 0;
                state = States.BaseState;
            }
        }
        else                    // elevate down
        {
            acceleration -= (fallingSpeed / 10) * Time.deltaTime;
            this.transform.Translate(Vector3.forward * acceleration);
            if (this.transform.position.y <= -elevatingMagnitude / 4 - elevatingMagnitude/2 * Random.value)
            {
                acceleration = 0;
                state = States.BaseState;
            }
        }
        
        
    }

    float PerlinNoiseMove(float x, float y)
    {
        x += Time.time * elevatingSpeed;
        y += Time.time * elevatingSpeed;
        return (Mathf.PerlinNoise(x, y) - .5f) * 2;
    }
}
