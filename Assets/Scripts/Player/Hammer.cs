using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class Hammer : MonoBehaviour
{
    [SerializeField] float rigCooldown;

    [Header("Do not touch")]
    [SerializeField] GameObject hammer;

    PlayerAbilities pa;
    ThirdPersonMovement mv;
    InputDecider id;
    HammerCheck hc;
    RigBuilder rigBuilder;

    bool hasHammer = false;

    private void Awake()
    {
        pa = GetComponent<PlayerAbilities>();
        mv = GetComponent<ThirdPersonMovement>();
        id = GetComponent<InputDecider>();
        hc = GetComponentInChildren<HammerCheck>();
        rigBuilder = GetComponentInChildren<RigBuilder>();
    }

    public void InitiateHammer()
    {
        hasHammer = true;
        hammer.SetActive(true);
        pa.SetHammer(true);
        id.SetHammer(true);
        rigBuilder.enabled = false;
    }

    public void Activate(InputAction.CallbackContext context)
    {
        mv.ActivateHammer();
        hc.SetHammer(true);
    }

    public void Punch()
    {
        mv.HammerPunch();
        hc.SetHammer(false);

        DeleteHammer(true);
    }

    public void DeleteHammer(bool hasCooldown)
    {
        hammer.SetActive(false);
        id.SetHammer(false);

        if (hasCooldown)
            StartCoroutine(RigCooldown());
        else
        {
            rigBuilder.enabled = true;
            pa.SetHammer(false);
        }
    }

    public void ResetHammer()
    {
        DeleteHammer(false);
        hc.DeleteHammer();
    }

    IEnumerator RigCooldown()
    {
        hasHammer = false;
        yield return new WaitForSeconds(rigCooldown);

        if (!hasHammer)
        {
            rigBuilder.enabled = true;
            pa.SetHammer(false);
        }
    }
}