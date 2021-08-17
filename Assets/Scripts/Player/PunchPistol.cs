using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchPistol : MonoBehaviour
{
    public void Punch(float radius, float zOffset)
    {
        Collider[] colls = Physics.OverlapSphere(transform.position  + new Vector3(0, 0, zOffset), radius, LayerMask.GetMask("Player"));
        List<GameObject> temps = new List<GameObject>();


        foreach (Collider coll in colls)
        {
            if (coll.gameObject.CompareTag("Player") && !temps.Contains(coll.gameObject))
            {
                coll.gameObject.GetComponent<ThirdPersonMovement>().GetRepelled(GetComponentInParent<ThirdPersonMovement>().gameObject.transform.position);
                temps.Add(coll.gameObject);
            }
        }
    }
}