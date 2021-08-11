using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    MeshRenderer mesh;
    ParticleSystem[] particles;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        particles = GetComponentsInChildren<ParticleSystem>();

        SetRocket(false);
        StopParticles();
    }

    public void SetRocket(bool value)
    {
        mesh.enabled = value;
    }

    public void StartParticles()
    {
        foreach (ParticleSystem p in particles)
        {
            p.Play();
        }
    }

    public void StopParticles()
    {
        foreach(ParticleSystem p in particles)
        {
            p.Stop();
        }
    }
}