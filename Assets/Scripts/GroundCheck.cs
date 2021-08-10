using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    bool isGrounded = false;
    ThirdPersonMovement movement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if (movement.HasJumped())
                movement.Land();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
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