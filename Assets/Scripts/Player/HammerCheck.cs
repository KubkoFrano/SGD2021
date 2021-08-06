using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerCheck : MonoBehaviour
{
    Hammer hammer;

    bool hasHammer = false;

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
            }
        }
    }

    public void SetHammer(bool value)
    {
        hasHammer = value;
    }
}