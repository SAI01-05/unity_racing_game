using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashToMenu : MonoBehaviour
{
    public AudioSource splashAudio; // audio reference

    void Start()
    {
        // Play audio
        if (splashAudio != null)
        {
            splashAudio.Play();
        }

        // Start coroutine
        StartCoroutine(ToMenu());
    }

    IEnumerator ToMenu()
    {
        // Wait for 4 seconds
        yield return new WaitForSeconds(4f);

        // Load Main Menu
        SceneManager.LoadScene(1);
    }
}