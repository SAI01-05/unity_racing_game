using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    // UI text object for countdown display
    public GameObject countDown;

    // Audio sources
    public AudioSource GetReady;   // Countdown sound (3,2,1)
    public AudioSource GoAudio;    // "Go" sound

    // Game elements to enable after countdown
    public GameObject LapTimer;
    public GameObject CarControls;
    public AudioSource LevelMusic;

    void Start()
    {
        // Start countdown coroutine
        StartCoroutine(CountStart());
    }

    IEnumerator CountStart()
    {
        // Initial delay before starting countdown
        yield return new WaitForSeconds(0.5f);

        // ---------------- COUNT 3 ----------------
        countDown.GetComponent<Text>().text = "3";
        GetReady.Play();
        countDown.SetActive(true);

        yield return new WaitForSeconds(1);
        countDown.SetActive(false);

        // ---------------- COUNT 2 ----------------
        countDown.GetComponent<Text>().text = "2";
        GetReady.Play();
        countDown.SetActive(true);

        yield return new WaitForSeconds(1);
        countDown.SetActive(false);

        // ---------------- COUNT 1 ----------------
        countDown.GetComponent<Text>().text = "1";
        GetReady.Play();
        countDown.SetActive(true);

        yield return new WaitForSeconds(1);
        countDown.SetActive(false);

        // ---------------- GO ----------------
        GoAudio.Play();

        // Start level music
        LevelMusic.Play();

        // Enable gameplay elements
        LapTimer.SetActive(true);
        CarControls.SetActive(true);
    }
}