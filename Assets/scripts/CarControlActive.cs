using UnityEngine;

public class CarControlActive : MonoBehaviour
{
    // Reference to player car controller
    public GameObject CarControl;

    // Reference to AI car controller
    public GameObject CarControlAI;

    void Start()
    {
        // Enable player car control script
        CarControl.GetComponent<CarContro>().enabled = true;

        // Enable AI car control script
        CarControlAI.GetComponent<AIWayPointCar>().enabled = true;
    }
}