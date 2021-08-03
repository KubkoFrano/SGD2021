using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchPistol : MonoBehaviour
{
    public void Punch(float radius)
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Player"));

        foreach (Collider coll in colls)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                coll.gameObject.GetComponent<ThirdPersonMovement>().GetRepelled(GetComponentInParent<ThirdPersonMovement>().gameObject.transform.position);
                Debug.Log("found");
            }
        }
    }
}