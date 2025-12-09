using UnityEngine;
using UnityEngine.UI; 

public class RoomManager : MonoBehaviour
{
    public GameObject checkmark1; // Drag the "Get out of bed" checkmark here
    public GameObject checkmark2; // Drag the "Flip Flops" checkmark here
    public GameObject checkmark3; // Drag the "Open Door" checkmark here
    public GameObject checkmark4; // Drag the "Leave Room" checkmark here


    // Call this at the end of your "Wake Up" Fade In animation
    public void TaskGetOutOfBedDone()
    {
        if (checkmark1 != null)
        {
            checkmark1.SetActive(true);
        }
    }

    // Call this when the player grabs the flip flops
    public void TaskFlipFlopsDone()
    {
        if (checkmark2 != null)
        {
            checkmark2.SetActive(true);
        }// Shows the checkmark
    }

    public void TaskOpenDoorDone()
    {
        if (checkmark3 != null)
        {
            checkmark3.SetActive(true);
        }
    }


        // This is called when the door is grabbed (from XR Grab Interactable)
        public void TaskDoorDone()
        {
            if (checkmark4 != null)
            {
                checkmark4.SetActive(true);
            }
            // NOTE: You can add logic here to load the next scene!
        }

        // (Keep the TaskFlipFlopsDone() function from before)
    
}