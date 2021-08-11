using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    MeshRenderer mesh;
    ParticleSystem particles;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        particles = GetComponentInChildren<ParticleSystem>();

        SetRocket(false);
        StopParticles();
    }

    public void SetRocket(bool value)
    {
        mesh.enabled = value;
    }

    public void StartParticles()
    {
        particles.Play();
    }

    public void StopParticles()
    {
        particles.Stop();
    }
}