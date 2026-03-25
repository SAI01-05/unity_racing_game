using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
     // Reference to AudioSource component (button click sound)
    public AudioSource clickSound;

      // Function to play click sound
    public void PlaySound()
    {
        // Safe play (scene change issues avoid karto)
        clickSound.PlayOneShot(clickSound.clip);
    }
}