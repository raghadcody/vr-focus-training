//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using TMPro; // Use TextMeshPro for better UI text

//public class LevelOneController : MonoBehaviour
//{
//    // --- Public References (Drag and Drop in Inspector) ---
//    [Header("UI References")]
//    public TextMeshProUGUI objectiveText; // Text UI element for objectives
//    public GameObject levelCanvas;          // The World Space Canvas GameObject

//    [Header("Game Object References")]
//    public OVRGrabbable flipFlopLeft;   // The Left Flip-flop (must have OVRGrabbable)
//    public OVRGrabbable flipFlopRight;  // The Right Flip-flop (must have OVRGrabbable)
//    public GameObject door;             // The door GameObject

//    [Header("Waking Animation Settings")]
//    public Animator characterAnimator;   // The Animator component on the character model
//    public string wakeUpTrigger = "WakeUp"; // The name of the trigger parameter in your Animator

//    // --- Private State ---
//    private enum GameState { WAKING, FIND_FLIPFLOPS, OPEN_DOOR, COMPLETE }
//    private GameState currentState = GameState.WAKING;
//    private int flipsGrabbedCount = 0;

//    // --- INITIALIZATION ---
//    void Start()
//    {
//        // 1. Initial State Setup
//        if (objectiveText == null)
//        {
//            Debug.LogError("Objective Text UI is not assigned! Game flow paused.");
//            return;
//        }

//        // 2. Start the sequence
//        StartCoroutine(StartLevelSequence());
//    }

//    // --- MAIN GAME FLOW COROUTINE ---
//    IEnumerator StartLevelSequence()
//    {
//        // Phase 1: Waking Up
//        UpdateObjective("Waking up...");
//        if (characterAnimator != null)
//        {
//            characterAnimator.SetTrigger(wakeUpTrigger);
//        }

//        // Wait for the animation to finish (adjust this time based on your animation length)
//        yield return new WaitForSeconds(3.0f);

//        // Phase 2: Find Flip-flops
//        currentState = GameState.FIND_FLIPFLOPS;
//        UpdateObjective("Objective: Find and wear your flip-flops (0/2)");

//        // FIX: The original code caused CS1061 errors because OVRGrabbable does not expose OnGrabBegin.
//        // We now add a helper script to monitor the OVRGrabbable.isGrabbed state.
//        flipFlopLeft.gameObject.AddComponent<FlipFlopGrabListener>().Init(this);
//        flipFlopRight.gameObject.AddComponent<FlipFlopGrabListener>().Init(this);
//    }

//    // --- STATE UPDATE METHODS ---

//    private void UpdateObjective(string newObjective)
//    {
//        objectiveText.text = newObjective;
//        Debug.Log("Game State: " + newObjective);
//    }

//    // Called by FlipFlopGrabListener when a flip-flop is grabbed for the first time
//    private void OnFlipFlopGrabbed()
//    {
//        if (currentState == GameState.FIND_FLIPFLOPS)
//        {
//            flipsGrabbedCount++;
//            UpdateObjective($"Objective: Find and wear your flip-flops ({flipsGrabbedCount}/2)");

//            // Check if both flips have been grabbed
//            if (flipsGrabbedCount >= 2)
//            {
//                // Remove the listeners by destroying the helper components
//                // This prevents the check from running after the objective is complete.
//                Destroy(flipFlopLeft.gameObject.GetComponent<FlipFlopGrabListener>());
//                Destroy(flipFlopRight.gameObject.GetComponent<FlipFlopGrabListener>());

//                // Advance to the next state
//                StartCoroutine(TransitionToDoorObjective());
//            }
//        }
//    }

//    IEnumerator TransitionToDoorObjective()
//    {
//        currentState = GameState.OPEN_DOOR;
//        UpdateObjective("Objective Complete! Now, go open the door.");

//        // Simple door functionality: when the player touches it, it opens
//        // For production, you'd use OVRGrabber on a door handle and a custom script for rotation

//        // Simple door activation (for testing):
//        // We will make the door open on its own after a small delay to move the player toward it
//        yield return new WaitForSeconds(2.0f);
//        OpenDoor();
//    }

//    private void OpenDoor()
//    {
//        // Use a simple animation or script to open the door (assuming it has a HingeJoint)
//        // For a HingeJoint, you would adjust the motor settings to rotate it.
//        // For simplicity, we will just disable the door for now (to let the player through).

//        if (door != null)
//        {
//            // A more realistic way would be to use a HingeJoint motor or a simple animation trigger.
//            // door.GetComponent<Animator>().SetTrigger("Open"); 

//            // For immediate testing: simply disable/move the door
//            door.transform.Rotate(0, 90f, 0);

//            StartCoroutine(LevelComplete());
//        }
//    }

//    IEnumerator LevelComplete()
//    {
//        currentState = GameState.COMPLETE;
//        UpdateObjective("Level 1 Complete! Great job.");
//        yield return new WaitForSeconds(5.0f);
//        UpdateObjective(""); // Clear objective
//        // TODO: Implement scene transition here (e.g., SceneManager.LoadScene("Level2");)
//    }

//    // Safety check for the door physics/trigger (if the player bumps it)
//    void OnCollisionEnter(Collision collision)
//    {
//        if (currentState == GameState.OPEN_DOOR && collision.gameObject == door)
//        {
//            // This is a backup for opening the door if the player touches it.
//            // In a real game, only the door handle grab would trigger this.
//            OpenDoor();
//        }
//    }

//    // --- INNER CLASS FOR OVRGRABBABLE MONITORING ---
//    // This helper class is dynamically added to the flip-flop GameObjects
//    private class FlipFlopGrabListener : MonoBehaviour
//    {
//        private LevelOneController controller;
//        private OVRGrabbable grabbable;
//        private bool grabbed = false; // Ensures the controller method is only called once per flip-flop

//        public void Init(LevelOneController controller)
//        {
//            this.controller = controller;
//            grabbable = GetComponent<OVRGrabbable>();
//        }

//        void Update()
//        {
//            // Check if it hasn't been grabbed yet, the OVRGrabbable component exists, 
//            // and the object is currently being grabbed.
//            if (!grabbed && grabbable != null && grabbable.isGrabbed)
//            {
//                grabbed = true;
//                // Notify the main controller script
//                controller.OnFlipFlopGrabbed();
//            }
//        }
//    }
////}