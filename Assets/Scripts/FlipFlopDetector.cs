using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FlipFlopDetector : MonoBehaviour
{
    [Header("References")]
    public LevelOneChecklistManager checklistManager;
    public GameObject FlipFlops;

    [Header("Audio Setup")]
    public AudioSource audioSource; 
    public AudioClip snapSound;    

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the sphere is the FlipFlops
        if (other.gameObject == FlipFlops)
        {
            WearFlipFlops();
        }
    }

    private void WearFlipFlops()
    {
        // 1. Play the sound (with a tiny bit of random pitch to make it sound natural)
        if (audioSource != null && snapSound != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(snapSound);
        }

        // 2. Tell the checklist manager the task is done
        if (checklistManager != null)
        {
            checklistManager.flibflobwore();
        }

        // 3. Force the XR Hand to let go
        XRGrabInteractable grabScript = FlipFlops.GetComponent<XRGrabInteractable>();
        if (grabScript != null)
        {
            grabScript.enabled = false;
        }

        // 4. Glue the flip-flops to the Feet_Trigger
        FlipFlops.transform.SetParent(this.transform);

        // 5. Center them on the feet
        FlipFlops.transform.localPosition = Vector3.zero;
        FlipFlops.transform.localRotation = Quaternion.identity;

        // 6. Physics cleanup: Stop them from falling or bumping into things
        Rigidbody rb = FlipFlops.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // This turns off gravity influence
            rb.useGravity = false;
        }

        // Disable the collider on the shoes so they don't hit the player's body
        Collider col = FlipFlops.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        // 7. Turn off this script so it doesn't try to "equip" them again
        this.enabled = false;
    }
}