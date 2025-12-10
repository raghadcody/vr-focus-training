using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelOneChecklistManager : MonoBehaviour
{
    // --- Public References (Set in Inspector) ---
    [Header("UI References")]
    // Drag your 4 Toggle GameObjects here IN ORDER (Toggle 0, Toggle 1, Toggle 2, Toggle 3)
    public Toggle[] taskToggles;
    public Button StartRoutinebutton ; // The Button component of the Start Button
    public GameObject lv1checklist; // The Canvas or container holding the checklist

    [Header("Level State")]
    private int currentTaskIndex = -1; // -1 means the routine hasn't started yet.

    void Start()
    {
        InitializeChecklist();
    }

    // Sets up the UI state before the routine begins
    public void InitializeChecklist()
    {
        currentTaskIndex = -1; // Routine not started

        // Hide all toggles and ensure checkmarks are off
        foreach (Toggle task in taskToggles)
        {
            // IMPORTANT: Hide the parent GameObject of the Toggle
            task.gameObject.SetActive(false);
            task.isOn = false;
        }

        // Show the UI container and the Start Button
        if (lv1checklist != null) lv1checklist.SetActive(true);
        if (StartRoutinebutton != null) StartRoutinebutton.gameObject.SetActive(true);
    }

    // --- FUNCTION CALLED BY THE START BUTTON ---
    public void StartRoutine()
    {
        if (currentTaskIndex == -1)
        {
            // 1. Start the first task sequence (Get out of Bed)
            currentTaskIndex = 0;

            // 2. Hide the Start Button
            if (StartRoutinebutton != null) StartRoutinebutton.gameObject.SetActive(false);

            // 3. Mark the first task (Get out of Bed) complete immediately upon starting
            CompleteTask(0);

            Debug.Log("Routine Started. Task 0 (Get out of Bed) Complete.");
        }
    }

    // --- FUNCTION CALLED BY VR INTERACTIONS (FlipFlops, Door Button) ---
    // This is the only public function the VR interactions will call.
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

        // 3. Advance to the next task index
        currentTaskIndex++;

        // 4. Check for Level/Routine Completion
        if (currentTaskIndex < taskToggles.Length)
        {
            // Show the next task in the sequence
            taskToggles[currentTaskIndex].gameObject.SetActive(true);
            Debug.Log("Next Task: " + currentTaskIndex + " is now visible.");
        }
        else
        {
            // LEVEL ONE IS COMPLETE!
            Debug.Log("Level One Routine Complete! Ready for Bathroom.");
            // TODO: Call a function here to load the next scene or open the Level 2 door.
        }
    }
}