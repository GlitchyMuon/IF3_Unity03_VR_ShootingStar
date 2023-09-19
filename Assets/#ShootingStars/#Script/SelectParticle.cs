using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectParticle : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public int particleIndex = 0;
    private void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];
        int numParticles = particleSystem.GetParticles(particles);

        if (particleIndex >= 0 && particleIndex < numParticles)
        {
            Vector3 particlePos = particles[particleIndex].position;

            Debug.Log("position : " + particlePos);
        }
    }
}
