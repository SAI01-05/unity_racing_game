using UnityEngine;

public class MenuAppear : MonoBehaviour
{
    // UI elements
    public GameObject largeButton;   // Initial big button
    public GameObject textClick;     // "Click to Start" text
    public GameObject menuButtons;   // Actual menu buttons (Play, Quit, etc.)

    public void StartMenu()
    {
        // Hide initial text
        textClick.SetActive(false);

        // Show menu buttons
        menuButtons.SetActive(true);

        // Hide large button
        largeButton.SetActive(false);
    }
}