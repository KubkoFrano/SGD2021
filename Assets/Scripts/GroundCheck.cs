using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] ParticleSystem walkParticles;
    [SerializeField] ParticleSystem landParticles;

    bool isGrounded = false;
    ThirdPersonMovement movement;

    private void Awake()
    {
        walkParticles.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            walkParticles.Play();
            landParticles.Play();

            if (movement.HasJumped())
                movement.Land();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

            walkParticles.Stop();
        }
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    public GroundCheck Assign(ThirdPersonMovement movement)
    {
        this.movement = movement;
        return this;
    }
}