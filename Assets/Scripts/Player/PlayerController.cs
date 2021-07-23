using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField][Range(0, 100)] private float interpolationPercentage;

    Vector3 move = Vector3.zero;
    Rigidbody rb;
    Vector3 targetVelocity = Vector3.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude < playerSpeed)
        {
            rb.velocity += move * (interpolationPercentage / 100);
        }
        else
        {
            rb.velocity = rb.velocity.normalized * playerSpeed;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        move = new Vector3(movement.x, 0, movement.y);

        if (context.canceled)
        {
            move = Vector3.zero;
        }
    }
}