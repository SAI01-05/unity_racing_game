using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeToMenu : MonoBehaviour
{
    void Update()
    {
        // Check if ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMainMenu();
        }
    }

    void GoToMainMenu()
    {
        // Load Main Menu scene (build index 1)
        SceneManager.LoadScene(1);
    }
}