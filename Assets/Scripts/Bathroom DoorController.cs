using UnityEngine;

public class BathroomDoorController : MonoBehaviour
{
    private Animator animator;


    [Header("Audio Setup")]
    public AudioSource audioSource; // The "CD Player" on the door
    public AudioClip openSound;    // Sound for opening
    public AudioClip closeSound;   // Sound for closing

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    public void OpenBathRoomDoor()
    {
        animator.SetBool("isbdopen", true);

        // Play the open sound
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }
    }

    public void CloseBathRoomDoor() 
    {
        animator.SetBool("isbdopen", false);

        // Play the close sound
        if (audioSource != null && closeSound != null)
        {
            audioSource.PlayOneShot(closeSound);
        }
    }


}
