using UnityEngine;

public class TapWaterControl : MonoBehaviour
{
    [Header("References")]
    public Transform upsinkpart; // Drag your handle here
    public ParticleSystem waterParticles;

    [Header("Settings")]
    public float openThreshold ; // The angle where water starts
    public bool reverseRotation = false; // Check this if your tap rotates negative

    void Update()
    {
        // Get the current rotation of the handle (X axis)
        float currentAngle = upsinkpart.localEulerAngles.x;

        // Convert 0-360 range to -180 to 180 for easier math
        if (currentAngle > 180) currentAngle -= 360;

        // Logic to play/stop particles
        bool isCurrentlyOpen = reverseRotation ? currentAngle < -openThreshold : currentAngle > -openThreshold;

        if (isCurrentlyOpen)
        {
            if (!waterParticles.isPlaying) waterParticles.Play();
        }
        else
        {
            if (waterParticles.isPlaying) waterParticles.Stop();
        }
    }
}