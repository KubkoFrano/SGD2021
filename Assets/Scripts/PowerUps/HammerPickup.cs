using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Hammer>()?.InitiateHammer();
            GetComponentInParent<PowerUpSpawner>().RemovePowerUp(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}