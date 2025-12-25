using UnityEngine;

public class TapWaterControl : MonoBehaviour
{
    [Header("References")]
    public Transform upsinkpart; // Drag your handle here
    public ParticleSystem waterParticles;
    public AudioSource waterAudioSource; // Drag the AudioSource here
    public AudioClip waterRunningSound; // Drag your water sound file here

    [Header("Settings")]
    public float openThreshold = 10f; // Angle where water starts
    public bool reverseRotation = false;

    void Start()
    {
        // Setup the audio source settings automatically
        if (waterAudioSource != null)
        {
            waterAudioSource.clip = waterRunningSound;
            waterAudioSource.loop = true; // Water should loop while running
            waterAudioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        // Get the current rotation of the handle (X axis)
        float currentAngle = upsinkpart.localEulerAngles.x;

        // Convert 0-360 range to -180 to 180
        if (currentAngle > 180) currentAngle -= 360;

        // Logic to play/stop particles and sound
        bool isCurrentlyOpen = reverseRotation ? currentAngle < -openThreshold : currentAngle > -openThreshold;

        if (isCurrentlyOpen)
        {
            // Start Particles
            if (!waterParticles.isPlaying) waterParticles.Play();

            // Start Sound
            if (waterAudioSource != null && !waterAudioSource.isPlaying)
            {
                waterAudioSource.Play();
            }
        }
        else
        {
            // Stop Particles
            if (waterParticles.isPlaying) waterParticles.Stop();

            // Stop Sound
            if (waterAudioSource != null && waterAudioSource.isPlaying)
            {
                waterAudioSource.Stop();
            }
        }
    }
}