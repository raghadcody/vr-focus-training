using UnityEngine;

public class BedroomDoorController : MonoBehaviour
{
    private Animator animator;

    [Header("Audio Setup")]
    public AudioSource audioSource; // The "CD Player" on the door
    public AudioClip openSound;    // Sound for opening
    public AudioClip closeSound;   // Sound for closing

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void OpenDoor()
    {
        animator.SetBool("isbdopen", true);

        // Play the open sound
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }
    }

    public void CloseDoor()
    {
        animator.SetBool("isbdopen", false);

        // Play the close sound
        if (audioSource != null && closeSound != null)
        {
            audioSource.PlayOneShot(closeSound);
        }
    }
}