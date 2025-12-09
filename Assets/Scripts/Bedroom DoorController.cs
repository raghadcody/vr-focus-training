using UnityEngine;

public class BedroomDoorController : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();


    }

    public void OpenDoor()
    {
        animator.SetBool("isbdopen", true);
    }

    public void CloseDoor()
    {
        animator.SetBool("isbdopen", false);
    }
}
