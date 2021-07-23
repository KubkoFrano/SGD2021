using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField][Range(0, 100)] private float interpolationPercentage;

    [Header("Camera")]
    [SerializeField] private float topBoundary;
    [SerializeField] private float bottomBoundary;
    [SerializeField] private float sensitivity;

    Vector3 move = Vector3.zero;
    Rigidbody rb;
    Transform holderTransform;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        holderTransform = GetComponentInChildren<CameraHolder>().gameObject.transform;
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

    public void ControlCamera(InputAction.CallbackContext context)
    {
        Vector2 cameraMovement = context.ReadValue<Vector2>();
        transform.Rotate(new Vector3(0, cameraMovement.x, 0) * Time.deltaTime * sensitivity, Space.Self);
        holderTransform.Rotate(new Vector3(-cameraMovement.y, 0, 0) * Time.deltaTime * sensitivity, Space.Self);

        Vector3 currentRotation = holderTransform.localRotation.eulerAngles;
        currentRotation.x = ConvertToAngle180(currentRotation.x);
        currentRotation.x = Mathf.Clamp(currentRotation.x, bottomBoundary, topBoundary);
        holderTransform.localRotation = Quaternion.Euler(currentRotation);
    }

    private float ConvertToAngle180(float input)
    {
        while (input > 360)
        {
            input -= 360;
        }
        while (input < -360)
        {
            input += 360;
        }
        if (input > 180)
        {
            input -= 360;
        }
        if (input < -180)
            input += input;
        return input;
    }
}