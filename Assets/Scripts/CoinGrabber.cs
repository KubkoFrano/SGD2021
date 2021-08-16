using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGrabber : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScore>()?.AddScore();
            ActivateShakeState();

            ClearCoin();
        }

        
        
    }
    

    void ClearCoin()
    {
        GetComponentInParent<GoldSpawner>().RemoveGold(this.gameObject);

        Destroy(this.gameObject);
    }

    void ActivateShakeState()
    {
        if (transform.parent.childCount == 1)
        {
            GetComponentInParent<BehaviourHexagon>().state.data.UpdateOnChange();
            GetComponentInParent<BehaviourHexagon>().state = new ShakeState(GetComponentInParent<BehaviourHexagon>().state.data, false);
        }
    }
}
