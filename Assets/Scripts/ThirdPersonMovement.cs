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

    [Header("Bird Hat")]
    [SerializeField] float birdHatForce;
    [SerializeField] float birdRiseSpeed;
    //[SerializeField] float birdHatDuration;
    [SerializeField] float birdAcceleration;
    [SerializeField] float maxBirdSpeed;
    [SerializeField] float buttonHoldTime;

    [SerializeField] float maxFuel;

    int baloonIndex;
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

    [Header("Do not touch")]
    [SerializeField] GameObject coin;

    Transform cameraTransform;
    GroundCheck groundCheck;
    Rigidbody rb;
    Rocket rocket;
    PlayerScore score;

    [SerializeField] Animator movementAnim;
    [SerializeField] Animator hatAnim;

    bool isRespawning = false;

    private void Start()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
        groundCheck = GetComponentInChildren<GroundCheck>().Assign(this);
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rocket = GetComponentInChildren<Rocket>();
        score = GetComponent<PlayerScore>();
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
            isBirding = false;
            return;
        }
        else if (!groundCheck.IsGrounded())
        {
            if (hasBird)
            {
                isBirding = true;
            } 
        }


        if (context.performed)
            return;

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
        if (score.SubtractScore())
        {
            CoinSpawnBehaviour tempCoin = Instantiate(coin, transform.position + Vector3.up, Quaternion.identity).GetComponent<CoinSpawnBehaviour>();
            tempCoin.SetOriginalPlayer(this.gameObject);
            tempCoin.SpawnCoin();
        }

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

    public void SetBaloonIndex(int index)
    {
        baloonIndex = index;
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
    }

    public void ActivateHammer()
    {
        if (!groundCheck.IsGrounded())
        {
            movementAnim.SetTrigger("hitHammer");
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.down * hammerDownForce, ForceMode.Impulse);
            ResetBird();
        }    
    }

    public void HammerPunch()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * hammerUpForce, ForceMode.Impulse);
        movementAnim.SetTrigger("recoverHammer");
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

    public void StartRespawning()
    {
        isRespawning = true;
    }

    public void StopRespawning()
    {
        isRespawning = false;
    }
}