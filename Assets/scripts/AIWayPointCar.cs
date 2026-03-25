using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AIWayPointCar : MonoBehaviour
{
    [Header("Waypoints Parent")]
    public Transform waypointParent;   // Parent object containing all waypoints

    [Header("Car Settings")]
    public float speed = 12f;          // Forward movement speed
    public float rotationSpeed = 6f;   // Turning smoothness
    public float reachDistance = 2.5f; // Distance to switch to next waypoint

    [Header("Wheel Colliders")]
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;

    [Header("Wheel Meshes")]
    public Transform frontLeftMesh;
    public Transform frontRightMesh;
    public Transform rearLeftMesh;
    public Transform rearRightMesh;

    // Internal waypoint system
    private Transform[] waypoints;
    private int currentIndex = 0;

    // Rigidbody reference
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // ---------------- PHYSICS STABILITY ----------------
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Lower center of mass for stability
        rb.centerOfMass = new Vector3(0, -0.5f, 0);

        // ---------------- LOAD WAYPOINTS ----------------
        int count = waypointParent.childCount;
        waypoints = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }
    }

    void FixedUpdate()
    {
        // If no waypoints assigned, exit
        if (waypoints.Length == 0) return;

        // Current target waypoint
        Transform target = waypoints[currentIndex];

        // ---------------- DIRECTION ----------------
        Vector3 direction = (target.position - transform.position).normalized;

        // ---------------- ROTATION ----------------
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        rb.MoveRotation(
            Quaternion.Slerp(
                rb.rotation,
                lookRotation,
                rotationSpeed * Time.fixedDeltaTime
            )
        );

        // ---------------- MOVEMENT ----------------
        rb.MovePosition(
            rb.position + transform.forward * speed * Time.fixedDeltaTime
        );

        // ---------------- WAYPOINT CHECK ----------------
        if (Vector3.Distance(transform.position, target.position) < reachDistance)
        {
            currentIndex++;

            // Loop back to first waypoint
            if (currentIndex >= waypoints.Length)
                currentIndex = 0;
        }

        // ---------------- WHEEL VISUAL UPDATE ----------------
        UpdateWheel(frontLeft, frontLeftMesh);
        UpdateWheel(frontRight, frontRightMesh);
        UpdateWheel(rearLeft, rearLeftMesh);
        UpdateWheel(rearRight, rearRightMesh);
    }

    void UpdateWheel(WheelCollider col, Transform mesh)
    {
        // Sync collider position & rotation with mesh
        Vector3 pos;
        Quaternion rot;

        col.GetWorldPose(out pos, out rot);

        mesh.position = pos;
        mesh.rotation = rot;
    }
}