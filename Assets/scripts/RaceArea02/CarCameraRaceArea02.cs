using UnityEngine;

public class CarCameraRaceArea02 : MonoBehaviour
{
    // Target (player car) reference
    public Transform target;
    public Rigidbody targetRb;

    [Header("Distance Settings")]
    public float distance = 6f;     // Default distance from car
    public float height = 2.5f;     // Camera height
    public float minDistance = 5f;  // Minimum zoom distance
    public float maxDistance = 8f;  // Maximum zoom distance

    [Header("Smooth Settings")]
    public float positionSmoothSpeed = 8f;   // Position smoothing
    public float rotationSmoothSpeed = 6f;   // Rotation smoothing

    [Header("Speed Zoom")]
    public float speedForMaxZoom = 100f;     // Speed required for max zoom out

    [Header("Drift Effect")]
    public float driftTilt = 5f;             // Camera tilt while drifting

    // Internal variable to store current distance
    private float currentDistance;

    void Start()
    {
        // Initialize camera distance
        currentDistance = distance;
    }

    void LateUpdate()
    {
        // If no target assigned, exit
        if (!target) return;

        // Get car speed (convert m/s to km/h)
        float speed = targetRb ? targetRb.linearVelocity.magnitude * 3.6f : 0f;

        // ---------------- DYNAMIC ZOOM ----------------
        // Calculate zoom factor based on speed
        float speedFactor = Mathf.Clamp01(speed / speedForMaxZoom);

        // Smoothly change camera distance
        currentDistance = Mathf.Lerp(minDistance, maxDistance, speedFactor);

        // ---------------- POSITION ----------------
        // Calculate desired camera position behind the car
        Vector3 desiredPosition = target.position
                                - target.forward * currentDistance
                                + Vector3.up * height;

        // Smoothly move camera to position
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            positionSmoothSpeed * Time.deltaTime
        );

        // ---------------- ROTATION ----------------
        // Look at the target
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);

        // Smoothly rotate camera
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            rotationSmoothSpeed * Time.deltaTime
        );

        // ---------------- DRIFT TILT ----------------
        // Get horizontal input for tilt effect
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate tilt amount
        float tilt = -horizontalInput * driftTilt;

        // Apply tilt to camera rotation
        transform.rotation *= Quaternion.Euler(0, 0, tilt);
    }
}