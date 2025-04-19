using UnityEngine;

namespace SoftHub.PhoenixFlame
{
    /// <summary>
    /// Controls the emission of a particle system, typically used for effects like fire or smoke.
    /// Provides methods to start and stop the emission of particles.
    /// </summary>
    public class ParticleControl : MonoBehaviour
    {
        [Tooltip("The ParticleSystem that controls the fire effect.")]
        [SerializeField] private ParticleSystem _fireParticles;

        /// <summary>
        /// Starts emitting particles from the ParticleSystem.
        /// This method plays the particle system, allowing particles to be emitted.
        /// </summary>
        public void StartEmission()
        {
            if (_fireParticles != null)
            {
                _fireParticles.Play();
            }
        }

        /// <summary>
        /// Stops emitting particles from the ParticleSystem.
        /// This method stops the particle system, halting new particles from being emitted.
        /// Existing particles will continue their lifecycle until they are finished.
        /// </summary>
        public void StopEmission()
        {
            if (_fireParticles != null)
            {
                _fireParticles.Stop();
            }
        }
    }
}
