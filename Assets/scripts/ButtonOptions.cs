using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonOptions : MonoBehaviour
{
    // ---------------- PLAY GAME ----------------
    public void PlayGame()
    {
        // Load main gameplay scene (build index 2)
        SceneManager.LoadScene(2);
    }

    // ---------------- MAIN MENU ----------------
    public void MainMenu()
    {
        // Load main menu scene (build index 1)
        SceneManager.LoadScene(1);
    }

    // ---------------- TRACK 01 ----------------
    public void Track01()
    {
        // Load Track 01 scene
        SceneManager.LoadScene(3);
    }

    // ---------------- TRACK 02 ----------------
    public void Track02()
    {
        // Load Track 02 scene
        SceneManager.LoadScene(4);
    }

    // ---------------- QUIT GAME ----------------
    public void QuitGame()
    {
        // Quit application (works in build, not in editor)
        Application.Quit();
    }
}