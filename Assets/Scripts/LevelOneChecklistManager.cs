using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class LevelOneChecklistManager : MonoBehaviour
{
    // --- Public References (Set in Inspector) ---
    [Header("UI References")]
    // Drag your 3 Toggle GameObjects here IN ORDER (0: Wakeup, 1: Flipflop, 2: Door)
    public Toggle[] taskToggles;
    public GameObject lv1checklist; // The Canvas or container holding the checklist

    // Optional UI element to show transient messages (set in Inspector)
    [Header("Message UI")]
    public TextMeshProUGUI messageText;
    public float messageDuration = 2f;

    public GameObject FlipFlops;

    [Header("Level State")]
    private int currentTaskIndex = -1; // -1 means the routine hasn't started yet.

    // Note: I recommend uncommenting InitializeChecklist() and Start() to reset the state on scene load!

    // --- FUNCTION CALLED BY THE START BUTTON ---
    public void StartRoutine()
    {
        if (currentTaskIndex == -1)
        {
            currentTaskIndex = 0;

            // Note: This block automatically completes Task 0.
            Toggle Wakeup = taskToggles[0];
            Wakeup.isOn = false; // This line is not needed
            CompleteTask(0);
            Wakeup.isOn = true; // This line is redundant as CompleteTask does it

            // The message for the NEXT step is handled by CompleteTask now.
            Debug.Log("Routine Started. Task 0 (Get out of Bed) Complete. Waiting for Flip-Flops (Task 1).");
        }
    }

    public void flibflobwore()
    {
        // Note: This block has sequencing issues (currentTaskIndex should be 1, not 0)
        if (currentTaskIndex == 1) // Should check if currentTaskIndex is the index of the task the player just completed (which is 1)
        {
            // Note: If you want to use CompleteTask(1), do not manually change currentTaskIndex here.
            // currentTaskIndex = 1; // Do not manually set this

            // Note: This block automatically completes Task 1.
            Toggle Flipflop = taskToggles[1];
            Flipflop.isOn = false; // This line is not needed

            CompleteTask(1); // Call CompleteTask with the ID of the task just done

            Flipflop.isOn = true; // This line is redundant as CompleteTask does it

            Debug.Log("Routine Started. Task 1 (wearflibflob) Complete. Waiting for open door.");
        }
    }

    public void opendoor()
    {
        // Note: This block has sequencing issues (currentTaskIndex should be 2, not 1)
        if (currentTaskIndex == 2) // Should check if currentTaskIndex is the index of the task the player just completed (which is 2)
        {
            // Note: This block automatically completes Task 2.
            Toggle Opendoor = taskToggles[2];
            Opendoor.isOn = false; // This line is not needed

            CompleteTask(2); // Call CompleteTask with the ID of the task just done

            Opendoor.isOn = true; // This line is redundant as CompleteTask does it

            Debug.Log("Routine Started. Task 2 (open door) Complete. Waiting for Bathroom.");
        }
    }

    // This function handles the visual checkmark, message display, and sequence advancement.
    public void CompleteTask(int completedTaskID)
    {
        // 1. Enforce Order: Check if the player completed the task we expected next.
        if (completedTaskID != currentTaskIndex)
        {
            Debug.LogWarning("Task sequence broken! Player tried to complete Task " + completedTaskID + " before Task " + currentTaskIndex);
            return;
        }

        // 2. Mark the current task as done (Visually check the box)
        if (currentTaskIndex >= 0 && currentTaskIndex < taskToggles.Length)
        {
            taskToggles[currentTaskIndex].isOn = true;
        }

        // 3. Advance to the next task index *before* checking the message and completion status
        // This ensures currentTaskIndex points to the NEXT task when we check completion and generate the message.
        currentTaskIndex++;

        // 4. Check for Level/Routine Completion
        if (currentTaskIndex < taskToggles.Length)
        {
            // --- Message Logic for the NEXT Task ---
            if (messageText != null)
            {
                string nextTaskName = "";
                switch (currentTaskIndex)
                {
                    case 1:
                        nextTaskName = "Wear the flip-flops";
                        break;
                    case 2:
                        nextTaskName = "Open the door";
                        break;
                    default:
                        nextTaskName = $"Do Task {currentTaskIndex}";
                        break;
                }
                StartCoroutine(ShowMessageCoroutine($"✅ Task {completedTaskID} Complete! Your next step is to: **{nextTaskName}**"));
            }

            // Show the next task in the sequence
            taskToggles[currentTaskIndex].gameObject.SetActive(true);
            Debug.Log("Next Task: " + currentTaskIndex + " is now visible and expected.");
        }
        else
        {
            // LEVEL ONE IS COMPLETE! (This triggers after the Door/Task 2 is completed)
            if (messageText != null)
            {
                StartCoroutine(ShowMessageCoroutine("🎉 Level One Complete! Proceed to the Bathroom."));
            }
            Debug.Log("Level One Routine Complete! Ready for Bathroom.");
            // TODO: Call a function here to load the next scene or open the Level 2 door.
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