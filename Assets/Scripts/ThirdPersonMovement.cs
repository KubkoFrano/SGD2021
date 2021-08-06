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
    [SerializeField] float repellAcceleration;
    [SerializeField] [Range(0, 1)] float slowDownAcc;
    [SerializeField] float repellMaxSpeed;

    [Header("Baloon")]
    [SerializeField] float buttonHoldTime;
    [SerializeField] float baloonForce;
    [SerializeField] float riseSpeed;
    [SerializeField] float maxBaloonFloatTime;
    [SerializeField] float baloonRechargeSpeed;
    [SerializeField] float baloonAcceleration;
    [SerializeField] float maxBaloonSpeed;

    bool isBalooning = false;
    float baloonFloatTime;
    int baloonIndex = -1;

    [Header("Bird Hat")]
    [SerializeField] float birdHatForce;
    [SerializeField] float birdRiseSpeed;
    [SerializeField] float birdHatDuration;
    [SerializeField] float birdAcceleration;
    [SerializeField] float maxBirdSpeed;

    bool hasBird = false;
    bool isBirding = false;

    [Header("Hammer")]
    [SerializeField] float hammerDownForce;
    [SerializeField] float hammerUpForce;

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
        baloonFloatTime = maxBaloonFloatTime;
        StartCoroutine(BaloonRecharge());
    }


    private void FixedUpdate()
    {
        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            if (isBirding)
                rb.AddForce(moveDirection.normalized * birdAcceleration);
            else if (isBalooning)
                rb.AddForce(moveDirection.normalized * baloonAcceleration);
            else
                rb.AddForce(moveDirection.normalized * (isRepelled ? repellAcceleration : acceleration));
        }

        if (isBirding)
        {
            if (rb.velocity.magnitude > maxBirdSpeed && !isRepelled)
            {
                Vector3 tempDir = new Vector3(moveDirection.normalized.x * maxBirdSpeed, rb.velocity.y, moveDirection.normalized.z * maxBirdSpeed);
                rb.velocity = tempDir;
            }
        }
        else if (isBalooning)
        {
            if (rb.velocity.magnitude > maxBaloonSpeed && !isRepelled)
            {
                Vector3 tempDir = new Vector3(moveDirection.normalized.x * maxBaloonSpeed, rb.velocity.y, moveDirection.normalized.z * maxBaloonSpeed);
                rb.velocity = tempDir;
            }
        }
        else
        {
            if (rb.velocity.magnitude > maxSpeed && !isRepelled)
            {
                Vector3 tempDir = new Vector3(moveDirection.normalized.x * maxSpeed, rb.velocity.y, moveDirection.normalized.z * maxSpeed);
                rb.velocity = tempDir;
            }
        }

        /*else if (isRepelled && rb.velocity.magnitude > repellMaxSpeed)
        {
            Vector3 temp = new Vector3((rb.velocity.normalized.x * repellMaxSpeed) + moveDirection.normalized.x * repellAcceleration, rb.velocity.y, (rb.velocity.normalized.z * repellMaxSpeed) + moveDirection.normalized.z * repellAcceleration);

            rb.velocity = temp;
        }*/
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
        if (context.canceled)
        {
            isBalooning = false;
            isBirding = false;
            return;
        }
        else
        {
            if (hasBird)
            {
                isBirding = true;
                isBalooning = false;
            } 
            else
                isBalooning = true;
        }


        if (context.performed)
            return;

        StartCoroutine(Baloon());
        StartCoroutine(Bird());

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

    IEnumerator Baloon()
    {
        yield return new WaitForSeconds(buttonHoldTime);

        while (baloonFloatTime > 0 && isBalooning)
        {
            rb.AddForce(Vector3.up * baloonForce, ForceMode.Force);

            if (rb.velocity.y > riseSpeed)
            {
                rb.velocity = new Vector3(rb.velocity.x, riseSpeed, rb.velocity.z);
            }

            baloonFloatTime -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Bird()
    {
        yield return new WaitForSeconds(buttonHoldTime);

        while (isBirding && hasBird)
        {
            rb.AddForce(Vector3.up * birdHatForce, ForceMode.Force);

            if (rb.velocity.y > birdRiseSpeed)
            {
                rb.velocity = new Vector3(rb.velocity.x, birdRiseSpeed, rb.velocity.z);
            }

            yield return new WaitForEndOfFrame();
        }

        isBirding = false;
    }

    IEnumerator BaloonRecharge()
    {
        while (true)
        {
            if (!isBalooning)
            {
                baloonFloatTime += baloonRechargeSpeed * Time.deltaTime;

                if (baloonFloatTime > maxBaloonFloatTime)
                    baloonFloatTime = maxBaloonFloatTime;
            }

            //Refresh UI
            if (baloonIndex != -1)
                App.inGameScreen.UpdateBaloon(baloonIndex, GetTimeNormalized());
            yield return new WaitForEndOfFrame();
        }
    }

    public void SetBaloonIndex(int index)
    {
        baloonIndex = index;
    }

    float GetTimeNormalized()
    {
        return baloonFloatTime / maxBaloonFloatTime;
    }

    public void StartBirdHat()
    {
        StartCoroutine(BirdHat());
    }

    IEnumerator BirdHat()
    {
        hasBird = true;
        App.inGameScreen.ToggleBirdSlider(baloonIndex, true);
        float timer = birdHatDuration;


        while (timer > 0)
        {
            timer -= Time.deltaTime;
            App.inGameScreen.UpdateBird(baloonIndex, timer / birdHatDuration);
            yield return new WaitForEndOfFrame();
        }

        App.inGameScreen.UpdateBird(baloonIndex, 0);

        hasBird = false;
        App.inGameScreen.ToggleBirdSlider(baloonIndex, false);
    }

    public void ActivateHammer()
    {
        if (!groundCheck.IsGrounded())
            rb.AddForce(Vector3.down * hammerDownForce, ForceMode.Impulse);
    }

    public void HammerPunch()
    {
        rb.AddForce(Vector3.up * hammerUpForce, ForceMode.Impulse);
    }
}