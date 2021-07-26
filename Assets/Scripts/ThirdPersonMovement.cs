using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float turnSmoothTime;
    [SerializeField] float gravity;
    [SerializeField] float jumpHeight;

    Vector3 movement = Vector3.zero;
    float turnSmoothVelocity;
    Vector3 velocity;

    CharacterController controller;
    Transform cameraTransform;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 temp = context.ReadValue<Vector2>();
        movement = new Vector3(temp.x, 0, temp.y).normalized;

        if (context.canceled)
            movement = Vector3.zero;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.canceled || context.performed || !controller.isGrounded)
            return;

        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}