using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDeleter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            Destroy(this.gameObject);
        }
    }
}