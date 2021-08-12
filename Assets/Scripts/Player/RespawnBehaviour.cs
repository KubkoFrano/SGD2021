using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBehaviour : MonoBehaviour
{
    Rigidbody rb;
    Hammer hammer;
    ThirdPersonMovement movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        hammer = GetComponent<Hammer>();
        movement = GetComponent<ThirdPersonMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            transform.position = App.playerManager.GetSpawnPositions()[Random.Range(0, 4)];
            rb.velocity = new Vector3(0, -100, 0);
            hammer.ResetHammer();
            movement.ResetBird();
            movement.ResetBaloon();
        }
    }
}