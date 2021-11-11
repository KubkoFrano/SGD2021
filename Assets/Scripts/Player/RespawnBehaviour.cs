using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RespawnBehaviour : MonoBehaviour
{
    [SerializeField] float recenteringTime;
    [SerializeField] float respawnMoveDelay;
    [SerializeField] CinemachineFreeLook cam;
    [SerializeField] GameObject respawnParticles;

    Rigidbody rb;
    Hammer hammer;
    ThirdPersonMovement movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        hammer = GetComponent<Hammer>();
        movement = GetComponent<ThirdPersonMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            transform.position = App.playerManager.GetSpawnPositions()[Random.Range(0, 4)];
            Instantiate(respawnParticles, transform.position, Quaternion.identity);
            rb.velocity = new Vector3(0, -100, 0);
            hammer.ResetHammer();
            movement.ResetBird();
            movement.ToIdle();

            Recenter();
        }
    }

    public void RecenterWithDelay(float delay)
    {
        StartCoroutine(Delay(delay));
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Recenter();
    }

    public void Recenter()
    {
        Vector2 tempPos = App.playerManager.GetCenterPosition();
        transform.LookAt(new Vector3(tempPos.x, transform.position.y, tempPos.y));
        StartCoroutine(ManageRecentering());
    }

    IEnumerator ManageRecentering()
    {
        movement.StartRespawning();
        cam.m_RecenterToTargetHeading.m_RecenteringTime = 0;
        yield return new WaitForSeconds(respawnMoveDelay);
        cam.m_RecenterToTargetHeading.m_RecenteringTime = recenteringTime;
        movement.StopRespawning();
    }
}