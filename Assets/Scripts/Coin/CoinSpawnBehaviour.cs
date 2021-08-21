using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnBehaviour : MonoBehaviour
{
    [SerializeField] float upForce;
    [SerializeField] float turnOnColliderAfter;

    [Header("Do not touch")]
    [SerializeField] SphereCollider coll;

    Rigidbody rb;
    CoinGrabber grabber;
    GameObject originalPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabber = GetComponentInChildren<CoinGrabber>();
    }

    public void SpawnCoin()
    {
        StartCoroutine(Initiate());
    }

    IEnumerator Initiate()
    {
        App.kingOfTheHill.AddCoin(this);
        rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
        yield return new WaitForSeconds(turnOnColliderAfter);
        coll.enabled = true;
    }

    public void SetOriginalPlayer(GameObject player)
    {
        grabber.SetOriginalPlayer(player);
    }
}