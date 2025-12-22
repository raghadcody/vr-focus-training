using UnityEngine;

public class ToothbrushLogic : MonoBehaviour
{
    public ParticleSystem foamParticles;
    public Rigidbody Toothbrush; // Drag the toothbrush Rigidbody here
    public float movementThreshold = 0.5f; // How fast you need to move to make foam

    private bool isInMouth = false;

    void Update()
    {
        if (isInMouth)
        {
            // Calculate how fast the brush is moving
            float speed = Toothbrush.linearVelocity.magnitude;

            // If moving fast enough, show foam. If stopped, hide foam.
            if (speed > movementThreshold)
            {
                if (!foamParticles.isPlaying) foamParticles.Play();
            }
            else
            {
                if (foamParticles.isPlaying) foamParticles.Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mouth")) isInMouth = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mouth"))
        {
            isInMouth = false;
            foamParticles.Stop(); // Always stop foam when leaving mouth
        }
    }
}