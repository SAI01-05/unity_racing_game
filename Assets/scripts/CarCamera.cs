using UnityEngine;

public class CarCamera : MonoBehaviour
{
    // Target (player car)
    public Transform target;

    [Header("Position Settings")]
    public float distance = 6f;  // Distance behind the car
    public float height = 3f;    // Height above the car

    [Header("Smooth Settings")]
    public float positionSmoothSpeed = 10f;  // Position smoothing speed
    public float rotationSmoothSpeed = 10f;  // Rotation smoothing speed

    void LateUpdate()
    {
        // If no target assigned, exit
        if (!target) return;

        // ---------------- POSITION ----------------
        // Calculate desired camera position behind the car
        Vector3 desiredPosition = target.position
                                  - target.forward * distance
                                  + Vector3.up * height;

        // Smoothly move camera to desired position
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            positionSmoothSpeed * Time.deltaTime
        );

        // ---------------- ROTATION ----------------
        // Calculate rotation to look at the target
        Quaternion desiredRotation = Quaternion.LookRotation(
            target.position - transform.position
        );

        // Smoothly rotate camera
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRotation,
            rotationSmoothSpeed * Time.deltaTime
        );
    }
}