using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDeleter : MonoBehaviour
{
    float lifetime;

    private void Start()
    {
        lifetime = GetComponent<ParticleSystem>().main.duration;

        StartCoroutine(LifetimeCounter());
    }

    IEnumerator LifetimeCounter()
    {
        yield return new WaitForSeconds(lifetime + 1);
        Destroy(gameObject);
    }
}