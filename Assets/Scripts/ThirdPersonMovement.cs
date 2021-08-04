using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;
    [SerializeField] float turnSmoothTime;
    [SerializeField] float jumpForce;
    [SerializeField] float repellForce;
    [SerializeField] float repellDuration;

    Vector3 movement = Vector3.zero;
    float turnSmoothVelocity;
    Vector3 moveDirection;
    bool isRepelled = false;

    Transform cameraTransform;
    GroundCheck groundCheck;
    Rigidbody rb;

    private void Start()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
        groundCheck = GetComponentInChildren<GroundCheck>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
    }


    private void FixedUpdate()
    {
        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            rb.AddForce(moveDirection.normalized * acceleration);
        }

        Vector3 tempMagnitude = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (rb.velocity.magnitude > maxSpeed && !isRepelled)
        {
            Vector3 tempDir = new Vector3(moveDirection.normalized.x * maxSpeed, rb.velocity.y, moveDirection.normalized.z * maxSpeed);
            rb.velocity = tempDir;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!App.gameManager.CompareGameState(GameState.game))
            return;

        Vector2 temp = context.ReadValue<Vector2>();
        movement = new Vector3(temp.x, 0, temp.y).normalized;

        if (context.canceled)
            movement = Vector3.zero;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!groundCheck.IsGrounded())
            return;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void GetRepelled(Vector3 otherPos)
    {
        StartCoroutine(RepellCooldown());
        Vector3 force = (transform.position - otherPos).normalized * repellForce;
        rb.AddForce(force, ForceMode.Impulse);
        Debug.Log(force);
    }

    IEnumerator RepellCooldown()
    {
        isRepelled = true;
        yield return new WaitForSeconds(repellDuration);
        isRepelled = false;
    }
}