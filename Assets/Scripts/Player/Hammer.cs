using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hammer : MonoBehaviour
{
    [Header("Do not touch")]
    [SerializeField] GameObject hammer;

    PlayerAbilities pa;
    ThirdPersonMovement mv;
    InputDecider id;
    HammerCheck hc;

    private void Awake()
    {
        pa = GetComponent<PlayerAbilities>();
        mv = GetComponent<ThirdPersonMovement>();
        id = GetComponent<InputDecider>();
        hc = GetComponentInChildren<HammerCheck>();
    }

    public void InitiateHammer()
    {
        hammer.SetActive(true);
        pa.SetHammer(true);
        id.SetHammer(true);
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

        DeleteHammer();
    }

    public void DeleteHammer()
    {
        hammer.SetActive(false);
        pa.SetHammer(false);
        id.SetHammer(false);
    }

    public void ResetHammer()
    {
        DeleteHammer();
        hc.DeleteHammer();
    }
}