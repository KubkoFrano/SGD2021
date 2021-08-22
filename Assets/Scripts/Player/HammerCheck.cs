using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerCheck : MonoBehaviour
{
    [SerializeField] GameObject hammerParticles;
    [SerializeField] Transform hitPoint;

    Hammer hammer;
    PlayerScore playerScore;

    bool hasHammer = false;
    bool isGrounded = false;

    private void Awake()
    {
        hammer = GetComponentInParent<Hammer>();
        playerScore = GetComponentInParent<PlayerScore>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (hasHammer)
            {
                hammer.Punch();
                
                other.gameObject.GetComponent<BehaviourHexagon>()?.Fall();
                playerScore.AddParticularScore(other.gameObject.GetComponent<BehaviourHexagon>().CarrotSmash());

                Instantiate(hammerParticles, hitPoint.position, Quaternion.Euler(-90, 0, 0));
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