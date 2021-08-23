using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchPistol : MonoBehaviour
{
    [SerializeField] GameObject punchParticles;
    [SerializeField] Transform particlesPosition;

    List<Collider> colls = new List<Collider>();
    ThirdPersonMovement movement;

    bool canPunch = true;

    [SerializeField] SkinnedMeshRenderer mesh;

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

        List<ThirdPersonMovement> tempT = new List<ThirdPersonMovement>();

        foreach (Collider c in colls)
        {
            ThirdPersonMovement temp = c.gameObject.GetComponent<ThirdPersonMovement>();
            if (movement != temp && !tempT.Contains(temp))
            {
                tempT.Add(temp);
                temp.GetRepelled(movement.gameObject.transform.position);
                playParticles = true;
            }
        }

        if (playParticles)
        {
            Instantiate(punchParticles, particlesPosition.position, Quaternion.identity);
            App.audioManager.Play("Punch");
        }
    }

    public void Toggle(bool canPunch)
    {
        mesh.enabled = canPunch;
        this.canPunch = canPunch;
    }
}