using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEffect : MonoBehaviour
{
    SyncFallState syncFall;
    //int PowerUpIndex;
    Transform[] playerTransforms;


    private void Start()
    {
        syncFall = GetComponentInParent<SyncFallState>();

        playerTransforms = App.playerManager.GetPlayerTransforms();
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        ifPlayerCollide(collision.gameObject.transform);

        GetComponentInParent<PowerUpSpawner>().RemovePowerUp(this.gameObject);
        Destroy(this.gameObject);
    }

    void ifPlayerCollide(Transform activePlayer)
    {
        List<Transform> enemies = new List<Transform>();
        for (int i = 0; i < playerTransforms.Length; i++)
        {
            if (playerTransforms[i] != activePlayer) enemies.Add(playerTransforms[i]);
        }


            RedPowerUP(enemies);

    }
    void RedPowerUP(List<Transform> enemies)
    {

        for (int i = 0; i < enemies.Count; i++)
        {
            syncFall.targetPlatform = new Vector2(enemies[i].position.x, enemies[i].position.z);     //give hire xz pozition of target enemi player
            syncFall.ShakeCentralDown();
        }
    }
}
