using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables; // Needed for XR Interactable types

public class DoorPhysicsManager : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        // Get references to the components on this same object (Door_Hinge)
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // IMPORTANT: The door must start as Kinematic so it doesn't flop around 
        // until the player touches it.
        rb.isKinematic = true;

        // Subscribe to the grab event
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(EnablePhysics);
        }
    }

    private void EnablePhysics(SelectEnterEventArgs args)
    {
        // When the player first grabs the door (Select Entered):
        // 1. Turn off Kinematic mode to allow the Rigidbody (and Hinge Joint) to work.
        rb.isKinematic = false;

        // 2. You only need to do this once, so unsubscribe from the event
        grabInteractable.selectEntered.RemoveListener(EnablePhysics);
    }

    void OnDestroy()
    {
        // Always clean up listeners if the object is destroyed
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(EnablePhysics);
        }
    }
}