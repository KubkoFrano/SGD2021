using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Punch pistol")]
    [SerializeField] float punchForce;
    [SerializeField] float punchCooldown;
    [SerializeField] float punchSeconds;
    [SerializeField] float checkRadius;
    [SerializeField] float zOffset;

   [Header("Do not touch")]
    [SerializeField] Animator anim;

    bool canPunch = true;

    PunchPistol pistol;

    private void Awake()
    {
        pistol = GetComponentInChildren<PunchPistol>();
    }

    private void Start()
    {
        //Temporarily vital
    }

    public void Punch(InputAction.CallbackContext context)
    {
        if (context.canceled || context.performed || !App.gameManager.CompareGameState(GameState.game) || !canPunch)
            return;

        StartCoroutine(ManagePunch());
        StartCoroutine(PunchCooldown());
        anim.SetTrigger("Punch");
    }

    IEnumerator PunchCooldown()
    {
        canPunch = false;
        yield return new WaitForSeconds(punchCooldown);
        canPunch = true;
    }

    IEnumerator ManagePunch()
    {
        yield return new WaitForSeconds(punchSeconds);
        pistol.Punch(checkRadius, zOffset);
    }
}