using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEffect : MonoBehaviour
{
    SyncFallState syncFall;
    public List<Transform> playerPosition = new List<Transform>();  // Asign here every player


    private void Start()
    {
        
        syncFall = this.transform.parent.parent.parent.GetComponent<SyncFallState>();

        playerPosition.Add(GameObject.Find("/TestPlayer 1").transform);
        playerPosition.Add(GameObject.Find("/TestPlayer 2").transform);
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        ifPlayerCollide(collision.gameObject.transform);


        Destroy(this.gameObject);
    }

    void ifPlayerCollide(Transform activePlayer)
    {
        List<Transform> enemies = new List<Transform>();
        for (int i = 0; i < playerPosition.Count; i++)
        {
            if (playerPosition[i] != activePlayer) enemies.Add(playerPosition[i]);
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
