using UnityEngine;
using UnityEngine.UI;

public class LapComplete : MonoBehaviour
{
    // Trigger references
    public GameObject LapCompleteTrig;
    public GameObject HalfLapTrig;

    // Time display UI
    public GameObject MinuteDisplay;
    public GameObject SecondDisplay;
    public GameObject MilliDisplay;

    // Lap counter UI
    public GameObject LapCounter;
    public int LapDone;

    // Best time tracking
    public float RawTime;

    // Race finish trigger
    public GameObject RaceFinish;

    void Update()
    {
        // Empty ठेवले (bug avoid)
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if player hits the trigger
        if (other.CompareTag("Player"))
        {
            // ---------------- LAP INCREMENT ----------------
            LapDone += 1;

            // Get saved best time
            RawTime = PlayerPrefs.GetFloat("RawTime");

            // ---------------- BEST TIME CHECK ----------------
            if (RawTime == 0 || LapTimeManager.RawTime < RawTime)
            {
                // Format seconds (add leading zero if needed)
                if (LapTimeManager.SecondCount <= 9)
                    SecondDisplay.GetComponent<Text>().text = "0" + LapTimeManager.SecondCount + ".";
                else
                    SecondDisplay.GetComponent<Text>().text = "" + LapTimeManager.SecondCount + ".";

                // Format minutes
                if (LapTimeManager.MinuteCount <= 9)
                    MinuteDisplay.GetComponent<Text>().text = "0" + LapTimeManager.MinuteCount + ":";
                else
                    MinuteDisplay.GetComponent<Text>().text = "" + LapTimeManager.MinuteCount + ":";

                // Milliseconds display
                MilliDisplay.GetComponent<Text>().text = "" + LapTimeManager.MilliCount;
            }

            // ---------------- SAVE TIME ----------------
            PlayerPrefs.SetInt("MinSave", LapTimeManager.MinuteCount);
            PlayerPrefs.SetInt("SecSave", LapTimeManager.SecondCount);
            PlayerPrefs.SetFloat("MilliSave", LapTimeManager.MilliCount);
            PlayerPrefs.SetFloat("RawTime", LapTimeManager.RawTime);

            // ---------------- FINISH CHECK ----------------
            if (LapDone == 2)
            {
                // Activate finish sequence
                RaceFinish.SetActive(true);

                // Hide timer UI
                MinuteDisplay.SetActive(false);
                SecondDisplay.SetActive(false);
                MilliDisplay.SetActive(false);

                // Stop timer
                LapTimeManager.TimerOn = false;
            }

            // ---------------- RESET LAP TIMER ----------------
            LapTimeManager.MinuteCount = 0;
            LapTimeManager.SecondCount = 0;
            LapTimeManager.MilliCount = 0;
            LapTimeManager.RawTime = 0;

            // ---------------- UPDATE LAP UI ----------------
            LapCounter.GetComponent<Text>().text = "" + LapDone;

            // ---------------- TRIGGER CONTROL ----------------
            HalfLapTrig.SetActive(true);
            LapCompleteTrig.SetActive(false);
        }
    }
}