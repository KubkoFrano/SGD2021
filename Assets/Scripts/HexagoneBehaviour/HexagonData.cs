using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonData : MonoBehaviour
{
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        spawnedPosition = this.transform.position;
        //Position = spawnedPosition;
    }
    public Rigidbody rb;

    public float timeSinceLastChange;
    public float elevateStateTime;

    public float shakeDuration = 3;
    public float smoothStart = 0;
    public float elevateSpeed = 0.75f;
    public float fallSpeed = 1;
    public float magnitude = 5f;
    public float acc = 0;

    public float lastRotationZ = 0;
    public float direction = 0;
    public float offsetY = 0;

    public int rotateItteration = 0;

    public Vector3 spawnedPosition;
    public Vector3 lastPosition;
    //public Vector3 Position;

    public bool shakeMultiplePlatforms = false;
    public bool neverFalls = false;
    public bool replaceOffset = false;
    public bool SpawnProps = true;
    public bool baseState = false;
    public bool spawnGolds = true;

    public float randomOff = 0;
    public float revivedHeight;

    public ParticleSystem hexFallParticle;

    
    public void UpdateOnChange()
    {
        if (Random.value > .5f) direction = 1;
        else direction = -1;

        timeSinceLastChange = 0;
        elevateStateTime = Random.value * 80;

        acc = 0;
        smoothStart = 0;

        lastRotationZ = this.transform.localEulerAngles.y;
        rotateItteration = Random.Range(0, 6);

        lastPosition = new Vector3(spawnedPosition.x, this.transform.position.y, spawnedPosition.z);

        shakeMultiplePlatforms = false;
        replaceOffset = false;

        revivedHeight = Random.Range(offsetY - magnitude/2, offsetY + magnitude/2);
        randomOff = Random.value;

        baseState = false;
    }

    public void activateNeighbours()
    {
        //this.transform.GetChild(HexGen.HexagoneList[index].transform.GetSiblingIndex())
        //this.transform.GetComponentInParent<SyncFallState>().ShakeThemDown();
        this.transform.parent.GetComponent<SyncFallState>().ShakeThemDown();
    }

}
