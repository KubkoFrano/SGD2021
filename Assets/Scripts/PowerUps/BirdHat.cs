using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdHat : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ThirdPersonMovement>()?.StartBirdHat();
            GetComponentInParent<PowerUpSpawner>().RemovePowerUp(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}