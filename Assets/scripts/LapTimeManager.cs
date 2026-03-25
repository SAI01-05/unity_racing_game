using UnityEngine;
using UnityEngine.UI;

public class LapTimeManager : MonoBehaviour
{
    // ---------------- TIME VARIABLES (STATIC) ----------------
    public static int MinuteCount;
    public static int SecondCount;
    public static float MilliCount;
    public static string MilliDisplay;

    // UI references
    public GameObject MinuteBox;
    public GameObject SecondBox;
    public GameObject MilliBox;

    // Raw total time (for best lap comparison)
    public static float RawTime;

    // Timer control
    public static bool TimerOn = true;

    void Update()
    {
        // Stop updating if timer is off
        if (!TimerOn) return;

        // ---------------- TIME CALCULATION ----------------
        MilliCount += Time.deltaTime * 10;   // Fast increment for milliseconds
        RawTime += Time.deltaTime;           // Real-time tracking

        // ---------------- MILLISECONDS DISPLAY ----------------
        MilliDisplay = MilliCount.ToString("F0");
        MilliBox.GetComponent<Text>().text = "" + MilliDisplay;

        // Convert milliseconds to seconds
        if (MilliCount >= 10)
        {
            MilliCount = 0;
            SecondCount += 1;
        }

        // ---------------- SECONDS DISPLAY ----------------
        if (SecondCount <= 9)
            SecondBox.GetComponent<Text>().text = "0" + SecondCount + ".";
        else
            SecondBox.GetComponent<Text>().text = "" + SecondCount + ".";

        // Convert seconds to minutes
        if (SecondCount >= 60)
        {
            SecondCount = 0;
            MinuteCount += 1;
        }

        // ---------------- MINUTES DISPLAY ----------------
        if (MinuteCount <= 9)
            MinuteBox.GetComponent<Text>().text = "0" + MinuteCount + ":";
        else
            MinuteBox.GetComponent<Text>().text = "" + MinuteCount + ":";
    }
}