using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelOneChecklistManager : MonoBehaviour
{
    [Header("UI References")]
    // Drag your 3 Bedroom Toggles here IN ORDER: Wakeup, flibflob, opendoor
    public Toggle[] taskToggles;
    public GameObject lv1checklist;

    [Header("Message UI")]
    public TextMeshProUGUI messageText;
    public float messageDuration = 2.5f;

    [Header("Level State")]
    // Start at 0 to match Level Two logic
    private int currentTaskIndex = 0;

    void Start()
    {
        InitializeLevelOne();
    }

    public void InitializeLevelOne()
    {
        currentTaskIndex = 0;

        for (int i = 0; i < taskToggles.Length; i++)
        {
            if (taskToggles[i] != null)
            {
                taskToggles[i].isOn = false;
                // Only show the first task if needed, or hide all until Start is pressed
                // If you want the checklist empty until Start, set all to false
                taskToggles[i].gameObject.SetActive(false);
            }
        }

        if (messageText != null)
            messageText.gameObject.SetActive(false);

        Debug.Log("Level One Initialized. Press Start to begin.");
    }

    // --- TASK FUNCTIONS (Called by VR Object Events) ---

    // Task 0: Start Routine (Wake Up)
    public void StartRoutine()
    {
        if (currentTaskIndex == 0)
        {
            // Note: We show the toggle first so CompleteTask can check it
            taskToggles[0].gameObject.SetActive(true);
            CompleteTask(0);
            Debug.Log("Routine Started. Task 0 (Wake Up) complete.");
        }
    }

    // Task 1: Wear Flip-Flops
    public void flibflobwore()
    {
        if (currentTaskIndex == 1)
        {
            CompleteTask(1);
            Debug.Log("Task 1 (Flip-Flops) complete.");
        }
    }

    // Task 2: Open Bedroom Door
    public void opendoor()
    {
        if (currentTaskIndex == 2)
        {
            CompleteTask(2);
            Debug.Log("Task 2 (Open Door) complete.");
        }
    }

    // --- CORE LOGIC (Identical to Level Two) ---

    public void CompleteTask(int completedTaskID)
    {
        // 1. Enforce Order
        if (completedTaskID != currentTaskIndex)
        {
            Debug.LogWarning($"Sequence broken! Tried Task {completedTaskID}, but expected {currentTaskIndex}");
            return;
        }

        // 2. Mark current toggle as checked
        if (taskToggles[currentTaskIndex] != null)
        {
            taskToggles[currentTaskIndex].isOn = true;
        }

        // 3. Move to next task
        currentTaskIndex++;

        // 4. Handle next step or finish
        if (currentTaskIndex < taskToggles.Length)
        {
            // Reveal the next toggle
            taskToggles[currentTaskIndex].gameObject.SetActive(true);

            // Set the instruction for the NEXT step
            string nextAction = "";
            switch (currentTaskIndex)
            {
                case 1: nextAction = "wear your flip-flops"; break;
                case 2: nextAction = "open the bedroom door"; break;
                default: nextAction = "proceed to next step"; break;
            }

            DisplayMessage($" Task {completedTaskID} Done! Next: <b>{nextAction}</b>");
        }
        else
        {
            // Level One Complete
            DisplayMessage("Level One Complete! Proceed to the Bathroom.");
            Debug.Log("All Level One tasks finished.");
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