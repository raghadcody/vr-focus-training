using UnityEngine;

public class FlipFlopDetector : MonoBehaviour
{
    [Header("References")]
    public LevelOneChecklistManager checklistManager;
    public GameObject FlipFlops;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the sphere is the FlipFlops
        if (other.gameObject == FlipFlops)
        {
            WearFlipFlops();
        }
    }

    private void WearFlipFlops()
    {
        Debug.Log("Flip-flops detected at feet. Wearing now...");

        // 1. Tell the checklist manager the task is done
        if (checklistManager != null)
        {
            checklistManager.flibflobwore();
        }

        // 2. Make the flip-flops "vanish"
        FlipFlops.SetActive(false);

        // 3. (Optional) Disable this detector so it doesn't trigger again
        this.gameObject.SetActive(false);
    }
}