using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelTwoChecklistManager : MonoBehaviour
{
    [Header("UI References")]
    // Drag your 5 Bathroom Toggles here IN ORDER
    public Toggle[] bathroomtasks;
    public GameObject lv2checklist; // The container for the bathroom UI

    [Header("Message UI")]
    public TextMeshProUGUI messageText;
    public float messageDuration = 3f;

    [Header("Level State")]
    private int currentTaskIndex = 0;

    void Start()
    {
        InitializeLevelTwo();
    }

    // Sets up the bathroom checklist state
    public void InitializeLevelTwo()
    {
        currentTaskIndex = 0;

        for (int i = 0; i < bathroomtasks.Length; i++)
        {
            if (bathroomtasks[i] != null)
            {
                // Set all to off initially
                bathroomtasks[i].isOn = false;

                // Only show the first task (Open Door), hide the rest
                bathroomtasks[i].gameObject.SetActive(i == 0);
            }
        }

        if (messageText != null)
            messageText.gameObject.SetActive(false);

        Debug.Log("Level Two Initialized. Waiting for Task 0: Open Door.");
    }

    // --- TASK FUNCTIONS (Called by VR Object Events) ---

    // Task 0: Open door
    public void Opendoor()
    {
        if (currentTaskIndex == 0)
        {
            CompleteTask(0);
        }
    }

    // Task 1: Open tap water
    public void opentabwater()
    {
        if (currentTaskIndex == 1)
        {
            CompleteTask(1);
        }
    }

    // Task 2: Grab toothbrush
    public void tethbrusggrab()
    {
        if (currentTaskIndex == 2)
        {
            CompleteTask(2);
        }
    }

    //// Task 3: Close tap water
    //public void closetabwater()
    //{
    //    if (currentTaskIndex == 3)
    //    {
    //        CompleteTask(3);
    //    }
    //}

    // Task 4: Close bathroom door
    public void closebathrromdoor()
    {
        if (currentTaskIndex == 3)
        {
            CompleteTask(3);
        }
    }

    // --- CORE LOGIC ---

    public void CompleteTask(int completedTaskID)
    {
        // 1. Enforce Order
        if (completedTaskID != currentTaskIndex)
        {
            Debug.LogWarning($"Sequence broken! Tried Task {completedTaskID}, but expected {currentTaskIndex}");
            return;
        }

        // 2. Mark current toggle as checked
        if (bathroomtasks[currentTaskIndex] != null)
        {
            bathroomtasks[currentTaskIndex].isOn = true;
        }

        // 3. Move to next task
        currentTaskIndex++;

        // 4. Handle next step or finish
        if (currentTaskIndex < bathroomtasks.Length)
        {
            // Show the next toggle in the list
            bathroomtasks[currentTaskIndex].gameObject.SetActive(true);

            // Determine the next instruction message
            string nextAction = "";
            switch (currentTaskIndex)
            {
                case 1: nextAction = "open the tap"; break;
                case 2: nextAction = "grab the toothbrush"; break;
                //case 3: nextAction = "close the tap"; break;
                case 3: nextAction = "close the bathroom door"; break;
            }

            DisplayMessage($"Task {completedTaskID} Done! Next: <b>{nextAction}</b>");
        }
        else
        {
            // Level Complete
            DisplayMessage("Level Two Complete! Proceed to the Kitchen.");
            Debug.Log("All Level Two tasks finished.");
        }
    }
    private void DisplayMessage(string msg)
    {
        Debug.Log("Displaying Message: " + msg); // Add this line!
        if (messageText != null)
        {
            StopAllCoroutines();
            StartCoroutine(ShowMessageCoroutine(msg));
        }
    }

    private IEnumerator ShowMessageCoroutine(string msg)
    {
        messageText.text = msg;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(messageDuration);
        messageText.gameObject.SetActive(false);
    }
}