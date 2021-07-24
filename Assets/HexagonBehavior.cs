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
        ElevateState,
        ShakeState,
        FallState,
        ReviveState
    }
    States state;
    float elevateStateTime;
    Vector3 spawnedPossition;

    private void Start()
    {
        elevateStateTime = Random.Range(0, 60);
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

            case States.ElevateState:
                ElevateState();
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
            startPos = this.transform.position.y;
            state = States.ElevateState;
        }
        else if (Random.Range(0, 1f) < 0.0001f)         //Change it later!!!
        {
            state = States.ShakeState;
        }

    }

    float smoothStart = 0;
    float elevatingDuration = 0;
    float startPos;
    void ElevateState()                                 // Do elevating State
    {
        if (smoothStart < 1) smoothStart += 0.001f;
        else smoothStart = 1;

        float y = PerlinNoiseMove(this.transform.position.x, this.transform.position.z) * smoothStart;
        this.transform.position = new Vector3(this.transform.position.x, (startPos * (1 - smoothStart)) + y * elevatingMagnitude, this.transform.position.z);

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
        this.transform.position = spawnedPossition + new Vector3(Random.Range(0f, .5f), 0, Random.Range(0f, .5f));
        
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

    float PerlinNoiseMove(float x, float y)
    {
        x += Time.time * elevatingSpeed;
        y += Time.time * elevatingSpeed;
        return (Mathf.PerlinNoise(x, y) - .5f) * 2;
    }
}
