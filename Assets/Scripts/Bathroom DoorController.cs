using UnityEngine;

public class BathroomDoorController : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    public void OpenBathRoomDoor()
    {
        animator.SetBool("isbdopen", true);
    }

    public void CloseBathRoomDoor() 
    {
        animator.SetBool("isbdopen", false);
    }


}
