using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class LevelOneChecklistManager : MonoBehaviour
{
    
    public Toggle[] taskToggles;
    public GameObject lv1checklist; 

    
    [Header("Message UI")]
    public TextMeshProUGUI messageText;
    public float messageDuration = 2f;

    public GameObject FlipFlops;

    [Header("Level State")]
    private int currentTaskIndex = -1; 

    
    public void StartRoutine()
    {
        if (currentTaskIndex == -1)
        {
            currentTaskIndex = 0;

            
            Toggle Wakeup = taskToggles[0];
            Wakeup.isOn = false; 
            CompleteTask(0);
            Wakeup.isOn = true; 

            
            Debug.Log("Routine Started. Task 0 (Get out of Bed) Complete. Waiting for Flip-Flops (Task 1).");
        }
    }

    public void flibflobwore()
    {
        
        if (currentTaskIndex == 1) 
        {
            
            Toggle Flipflop = taskToggles[1];
            Flipflop.isOn = false; 

            CompleteTask(1); 

            Flipflop.isOn = true; 

            Debug.Log("Routine Started. Task 1 (wearflibflob) Complete. Waiting for open door.");
        }
    }

    public void opendoor()
    {
        
        if (currentTaskIndex == 2) 
        {
            
            Toggle Opendoor = taskToggles[2];
            Opendoor.isOn = false; 

            CompleteTask(2); 

            Opendoor.isOn = true; 

            Debug.Log("Routine Started. Task 2 (open door) Complete. Waiting for Bathroom.");
        }
    }

    
    public void CompleteTask(int completedTaskID)
    {
        
        if (completedTaskID != currentTaskIndex)
        {
            Debug.LogWarning("Task sequence broken! Player tried to complete Task " + completedTaskID + " before Task " + currentTaskIndex);
            return;
        }

        
        if (currentTaskIndex >= 0 && currentTaskIndex < taskToggles.Length)
        {
            taskToggles[currentTaskIndex].isOn = true;
        }

        
        
        currentTaskIndex++;

        
        if (currentTaskIndex < taskToggles.Length)
        {
            
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

            
            taskToggles[currentTaskIndex].gameObject.SetActive(true);
            Debug.Log("Next Task: " + currentTaskIndex + " is now visible and expected.");
        }
        else
        {
            
            if (messageText != null)
            {
                StartCoroutine(ShowMessageCoroutine("🎉 Level One Complete! Proceed to the Bathroom."));
            }
            Debug.Log("Level One Routine Complete! Ready for Bathroom.");
           
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