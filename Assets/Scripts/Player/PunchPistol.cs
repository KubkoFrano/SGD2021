using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchPistol : MonoBehaviour
{
    [SerializeField] GameObject punchParticles;
    [SerializeField] Transform particlesPosition;

    List<Collider> colls = new List<Collider>();
    ThirdPersonMovement movement;

    private void Start()
    {
        movement = GetComponentInParent<ThirdPersonMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            colls.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            colls.Remove(other);
    }

    public void Punch()
    {
        bool playParticles = false;

        foreach (Collider c in colls)
        {
            ThirdPersonMovement temp = c.gameObject.GetComponent<ThirdPersonMovement>();
            if (movement != temp)
            {
                temp.GetRepelled(movement.gameObject.transform.position);
                playParticles = true;
                Debug.Log("Pow");
            }
        }

        if (playParticles)
            Instantiate(punchParticles, particlesPosition.position, Quaternion.identity);
    }
}