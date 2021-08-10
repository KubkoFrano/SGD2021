using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerCheck : MonoBehaviour
{
    Hammer hammer;

    bool hasHammer = false;
    bool isGrounded = false;

    private void Awake()
    {
        hammer = GetComponentInParent<Hammer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (hasHammer)
            {
                hammer.Punch();
                other.gameObject.GetComponent<BehaviourHexagon>()?.Fall();
            }

            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void DeleteHammer()
    {
        hasHammer = false;
    }

    public void SetHammer(bool value)
    {
        if (!isGrounded)
            hasHammer = value;
    }
}