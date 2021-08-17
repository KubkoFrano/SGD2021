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
    [SerializeField] float startStoppingAfter;
    [SerializeField] float stopForce;

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
    //[SerializeField] float birdHatDuration;
    [SerializeField] float birdAcceleration;
    [SerializeField] float maxBirdSpeed;

    [SerializeField] float maxFuel;

    bool hasBird = false;
    bool isBirding = false;

    float birdFuel = 0;

    [Header("Hammer")]
    [SerializeField] float hammerDownForce;
    [SerializeField] float hammerUpForce;

    Vector3 movement = Vector3.zero;
    float turnSmoothVelocity;
    Vector3 moveDirection;
    bool isRepelled = false;
    bool hasJumped = false;

    Transform cameraTransform;
    GroundCheck groundCheck;
    Rigidbody rb;
    Rocket rocket;

    Animator movementAnim;

    bool isRespawning = false;

    private void Start()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
        groundCheck = GetComponentInChildren<GroundCheck>().Assign(this);
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        baloonFloatTime = maxBaloonFloatTime;
        StartCoroutine(BaloonRecharge());
        rocket = GetComponentInChildren<Rocket>();
    }


    private void FixedUpdate()
    {
        if (movement.magnitude >= 0.1f && !isRespawning)
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

            movementAnim.SetBool("isRunning", true);
        }
        else
            movementAnim.SetBool("isRunning", false);
            
        float tempMag = Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.z * rb.velocity.z);
        float tempSpeed;

        if (isBirding)
            tempSpeed = maxBirdSpeed;
        if (isBalooning)
            tempSpeed = maxBaloonSpeed;
        else
            tempSpeed = maxSpeed;

        if (tempMag > tempSpeed && !isRepelled)
        {
            Vector3 tempDir = new Vector3(moveDirection.normalized.x * tempSpeed, rb.velocity.y, moveDirection.normalized.z * tempSpeed);
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
        if (context.canceled)
        {
            isBalooning = false;
            isBirding = false;
            return;
        }
        else if (!groundCheck.IsGrounded())
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

        StartCoroutine(Baloon(true));
        StartCoroutine(Bird());

        if (!groundCheck.IsGrounded())
            return;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        if (!hasJumped)
        {
            movementAnim.SetTrigger("jump");
            hasJumped = true;
        }
        
    }

    public void GetRepelled(Vector3 otherPos)
    {
        StartCoroutine(RepellCooldown());
        Vector3 force = (transform.position - otherPos).normalized * repellForce;

        if (groundCheck.IsGrounded())
            force = new Vector3(force.x, 0, force.z);

        ResetBird();
        rb.AddForce(force, ForceMode.Impulse);
    }

    IEnumerator RepellCooldown()
    {
        isRepelled = true;
        yield return new WaitForSeconds(startStoppingAfter);
        rb.AddForce(new Vector3(-rb.velocity.x, 0, -rb.velocity.y) * stopForce);
        yield return new WaitForSeconds(repellDuration - startStoppingAfter);
        isRepelled = false;
    }

    IEnumerator Baloon(bool wait)
    {
        if (wait)
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
        if (groundCheck.IsGrounded())
            yield return new WaitForSeconds(buttonHoldTime);

        if (isBirding && hasBird)
            rocket.StartParticles();

        while (isBirding && hasBird)
        {
            if (rb.velocity.y < 0)
                rb.AddForce(Vector3.up, ForceMode.Impulse);

            rb.AddForce(Vector3.up * birdHatForce, ForceMode.Force);
            birdFuel -= Time.deltaTime;

            if (rb.velocity.y > birdRiseSpeed)
            {
                rb.velocity = new Vector3(rb.velocity.x, birdRiseSpeed, rb.velocity.z);
            }

            yield return new WaitForEndOfFrame();
        }

        rocket.StopParticles();

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
        if (hasBird)
            birdFuel = maxFuel;
        else
            StartCoroutine(BirdHat());
    }

    IEnumerator BirdHat()
    {
        rocket.SetRocket(true);

        if (isBalooning)
        {
            StopCoroutine(Baloon(true));
            isBalooning = false;
            isBirding = true;
            StartCoroutine(Bird());
        }

        birdFuel = maxFuel;
        hasBird = true;
        App.inGameScreen.ToggleBirdSlider(baloonIndex, true);


        while (birdFuel > 0)
        {
            App.inGameScreen.UpdateBird(baloonIndex, birdFuel / maxFuel);
            yield return new WaitForEndOfFrame();
        }

        App.inGameScreen.UpdateBird(baloonIndex, 0);

        hasBird = false;
        App.inGameScreen.ToggleBirdSlider(baloonIndex, false);

        rocket.SetRocket(false);

        if (isBirding)
        {
            isBalooning = true;
            StartCoroutine(Baloon(false));
        }
    }

    public void ActivateHammer()
    {
        if (!groundCheck.IsGrounded())
        {
            isBalooning = false;
            StopCoroutine(Baloon(true));
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.down * hammerDownForce, ForceMode.Impulse);
            ResetBird();
        }    
    }

    public void HammerPunch()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * hammerUpForce, ForceMode.Impulse);
    }

    public void Land()
    {
        movementAnim.SetTrigger("land");
        hasJumped = false;
    }

    public bool HasJumped()
    {
        return hasJumped;
    }

    public void ResetBird()
    {
        hasBird = false;
        isBirding = false;
        StopCoroutine(Bird());
        StopCoroutine(BirdHat());
        App.inGameScreen.ToggleBirdSlider(baloonIndex, false);
        rocket.SetRocket(false);
    }

    public void ResetBaloon()
    {
        isBalooning = false;
        StopCoroutine(Baloon(true));

        baloonFloatTime = maxBaloonFloatTime;
        App.inGameScreen.UpdateBaloon(baloonIndex, GetTimeNormalized());
    }

    public void SetMovementAnim(Animator anim)
    {
        movementAnim = anim;
    }

    public void StartRespawning()
    {
        isRespawning = true;
    }

    public void StopRespawning()
    {
        isRespawning = false;
    }
}