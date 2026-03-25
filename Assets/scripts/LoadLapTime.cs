using UnityEngine;
using UnityEngine.UI;

public class LoadLapTime : MonoBehaviour
{
    // Stored time values
    public int MinCount;
    public int SecCount;
    public float MilliCount;

    // UI display references
    public GameObject MinDisplay;
    public GameObject SecDisplay;
    public GameObject MilliDisplay;

    void Start()
    {
        // ---------------- LOAD SAVED DATA ----------------
        MinCount = PlayerPrefs.GetInt("MinSave");
        SecCount = PlayerPrefs.GetInt("SecSave");
        MilliCount = PlayerPrefs.GetFloat("MilliSave");

        // ---------------- UPDATE UI ----------------
        MinDisplay.GetComponent<Text>().text = "" + MinCount + ":";
        SecDisplay.GetComponent<Text>().text = "" + SecCount + ".";
        MilliDisplay.GetComponent<Text>().text = "" + MilliCount;
    }
}