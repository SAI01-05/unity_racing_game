using UnityEngine;
using UnityEngine.UI;

public class PosBack : MonoBehaviour
{
    // Reference to UI Text that shows player position
    public GameObject positionDisplay;

    void OnTriggerExit(Collider other)
    {
        // Check if the object exiting trigger has tag "CarPos"
        if (other.CompareTag("CarPos"))
        {
            // Update UI text to show player is in 2nd place
            positionDisplay.GetComponent<Text>().text = "2nd Place";
        }
    }
}