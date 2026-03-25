using UnityEngine;

public class HalfPointTrigger : MonoBehaviour
{
    // Reference to lap complete trigger
    public GameObject LapCompleteTrig;

    // Reference to this half lap trigger
    public GameObject HalfLapTrig;

    void OnTriggerEnter(Collider other)
    {
        // Check if player entered the trigger
        if (other.CompareTag("Player"))
        {
            // Activate lap complete trigger
            LapCompleteTrig.SetActive(true);

            // Disable half lap trigger
            HalfLapTrig.SetActive(false);
        }
    }
}