using UnityEngine;

namespace App
{
    public class SelfDestroyEffect : MonoBehaviour
    {
        private ParticleSystem effect;

        void Start()
        {
            effect = GetComponent<ParticleSystem>();
        }

        private void OnParticleSystemStopped()
        {
            Destroy(this.gameObject);
        }
    }
}