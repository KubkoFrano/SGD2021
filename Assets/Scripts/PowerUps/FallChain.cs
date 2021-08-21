using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FallChain : MonoBehaviour
{
    SyncFallState syncFall;
    //int PowerUpIndex;
    Transform[] playerTransforms;
    PlayerScore[] scores;

    private void Start()
    {
        syncFall = GetComponentInParent<SyncFallState>();

        playerTransforms = App.playerManager.GetPlayerTransforms();
        scores = App.playerManager.GetPlayerScores();
    }

    private void OnTriggerEnter(Collider collision)
    {

        RedPowerUP();

        ClearPowerUp();
    }

    void RedPowerUP()
    {
        Vector2 target;
        Array.Sort(scores, new Comparison<PlayerScore>(
                  (i1, i2) => i1.GetScore().CompareTo(i2.GetScore())));
        Array.Reverse(scores);

        

        if      (UnityEngine.Random.value < .5 * (4 / scores.Length)) target = new Vector2(scores[0].transform.position.x, scores[0].transform.position.z);
        else if (UnityEngine.Random.value < .8 * (4 / scores.Length)) target = new Vector2(scores[1].transform.position.x, scores[1].transform.position.z);
        else                                                          target = new Vector2(scores[2].transform.position.x, scores[2].transform.position.z);



        syncFall.targetPlatform = target;     //give hire xz pozition of target enemi player
        syncFall.ShakeCentralDown();
        
    }

    void ClearPowerUp()
    {
        GetComponentInParent<PowerUpSpawner>().RemovePowerUp(this.gameObject);
        Destroy(this.gameObject);
    }
}
