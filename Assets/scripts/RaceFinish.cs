using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceFinish : MonoBehaviour
{
    // Player car reference
    public GameObject MyCar;

    // Camera system
    public GameObject FinishCam;
    public GameObject ViewModes;
    public GameObject FinishCamHolder;

    // AI car reference
    public GameObject AICar;

    // Audio & triggers
    public GameObject LevelMusic;
    public GameObject CompleteTrig;
    public AudioSource FinishMusic;

    // UI reference
    public GameObject GameUI;

    // Lap tracking script
    public LapComplete lapScript;

    void OnTriggerEnter(Collider other)
    {
        // Check if player finished required laps
        if (other.CompareTag("Player") && lapScript.LapDone == 2)
        {
            // ---------------- CAMERA SWITCH ----------------
            FinishCamHolder.SetActive(true);
            ViewModes.SetActive(false);
            FinishCam.SetActive(false);

            // ---------------- PLAYER CAR STOP ----------------
            Rigidbody playerRb = MyCar.GetComponent<Rigidbody>();

            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            playerRb.isKinematic = true;

            // ---------------- AI CAR STOP ----------------
            Rigidbody aiRb = AICar.GetComponent<Rigidbody>();

            aiRb.linearVelocity = Vector3.zero;
            aiRb.angularVelocity = Vector3.zero;
            aiRb.isKinematic = true;

            // Disable AI movement script
            AICar.GetComponent<AIWayPointCar>().enabled = false;

            // ---------------- AUDIO & TRIGGERS ----------------
            LevelMusic.SetActive(false);
            CompleteTrig.SetActive(false);

            // ---------------- UI OFF ----------------
            GameUI.SetActive(false);

            // ---------------- FINISH MUSIC ----------------
            FinishMusic.Play();

            // ---------------- RETURN TO MENU ----------------
            // Load main menu after 5 seconds
            Invoke("GoToMainMenu", 5f);
        }
    }

    void GoToMainMenu()
    {
        // Load Main Menu scene (by name)
        SceneManager.LoadScene("MainMenu");
    }
}