using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGrabber : MonoBehaviour
{
    [SerializeField] float pickupCooldown;

    GameObject originalPlayer;

    bool canPickup = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && (!originalPlayer || ComparePlayer(collision.gameObject)))
        {
            GetComponent<CapsuleCollider>().enabled = false;
            collision.gameObject.GetComponent<PlayerScore>()?.AddScore();

            if (!originalPlayer)
                ActivateShakeState();

            ClearCoin();
        }

        
        
    }
    

    void ClearCoin()
    {
        if (!originalPlayer)
            GetComponentInParent<GoldSpawner>().RemoveGold(this.gameObject);

        App.kingOfTheHill.RemoveCoin(GetComponent<CoinSpawnBehaviour>());
        Destroy(transform.parent.gameObject);
    }

    void ActivateShakeState()
    {
        if (transform.parent.parent.childCount == 1)
        {
            GetComponentInParent<BehaviourHexagon>().state.data.UpdateOnChange();
            GetComponentInParent<BehaviourHexagon>().state = new ShakeState(GetComponentInParent<BehaviourHexagon>().state.data, false);
        }
    }

    public void SetOriginalPlayer(GameObject player)
    {
        originalPlayer = player;
        StartCoroutine(PlayerCooldown());
    }

    IEnumerator PlayerCooldown()
    {
        yield return new WaitForSeconds(pickupCooldown);
        canPickup = true;
    }

    bool ComparePlayer(GameObject player)
    {
        if (canPickup)
            return true;
        else
            return originalPlayer != player;
    }
}
