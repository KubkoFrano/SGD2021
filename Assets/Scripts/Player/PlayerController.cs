using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody rb;
    Vector3 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ManageMove());
    }



    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        movement = new Vector3(movement.x, 0, movement.y);
    }

    IEnumerator ManageMove()
    {
        while (true)
        {
            rb.velocity = movement * Time.deltaTime * speed * 100;
            yield return new WaitForEndOfFrame();
        }
    }
}