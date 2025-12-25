using UnityEngine;

public class ToothbrushLogic : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem foamParticles;
    public Rigidbody toothbrushRigidbody;
    public AudioSource scrubAudioSource; // Drag an AudioSource here
    public AudioClip scrubSound;         // Drag your scrubbing sound file here

    [Header("Settings")]
    public float movementThreshold = 0.1f;

    private bool isInMouth = false;

    void Start()
    {
        if (foamParticles != null) foamParticles.Stop();

        // Setup AudioSource for looping
        if (scrubAudioSource != null)
        {
            scrubAudioSource.clip = scrubSound;
            scrubAudioSource.loop = true;
            scrubAudioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        if (isInMouth && toothbrushRigidbody != null)
        {
            float speed = toothbrushRigidbody.linearVelocity.magnitude;

            if (speed > movementThreshold)
            {
                // Play Foam
                if (!foamParticles.isPlaying) foamParticles.Play();

                // Play Sound
                if (!scrubAudioSource.isPlaying) scrubAudioSource.Play();
            }
            else
            {
                // Stop Foam
                if (foamParticles.isPlaying) foamParticles.Stop();

                // Stop Sound
                if (scrubAudioSource.isPlaying) scrubAudioSource.Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Mouth_Trigger" || other.CompareTag("Mouth"))
        {
            isInMouth = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Mouth_Trigger" || other.CompareTag("Mouth"))
        {
            isInMouth = false;
            if (foamParticles != null) foamParticles.Stop();
            if (scrubAudioSource != null) scrubAudioSource.Stop();
        }
    }
}