using System.Collections;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    // Camera references
    public GameObject NormalCam;
    public GameObject FarCam;
    public GameObject FPCam;

    // Current camera mode (0 = Normal, 1 = Far, 2 = FP)
    public int camMode;

    void Update()
    {
        // Check for camera switch input
        if (Input.GetButtonDown("viewmode"))
        {
            // Cycle between 0 → 1 → 2 → 0
            if (camMode == 2)
            {
                camMode = 0;
            }
            else
            {
                camMode += 1;
            }

            // Start camera change coroutine
            StartCoroutine(ModeChange());
        }
    }

    IEnumerator ModeChange()
    {
        // Small delay before switching
        yield return new WaitForSeconds(0.01f);

        // ---------------- NORMAL CAMERA ----------------
        if (camMode == 0)
        {
            NormalCam.SetActive(true);
            FPCam.SetActive(false);
        }

        // ---------------- FAR CAMERA ----------------
        if (camMode == 1)
        {
            FarCam.SetActive(true);
            NormalCam.SetActive(false);
        }

        // ---------------- FIRST PERSON CAMERA ----------------
        if (camMode == 2)
        {
            FPCam.SetActive(true);
            FarCam.SetActive(false);
        }
    }
}