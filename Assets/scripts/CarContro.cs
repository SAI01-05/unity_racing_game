using UnityEngine;

public class CarContro : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    [Header("Wheel Meshes")]
    public Transform frontLeftVisual;
    public Transform frontRightVisual;
    public Transform rearLeftVisual;
    public Transform rearRightVisual;

    [Header("Car Settings")]
    public float maxMotorTorque = 1500f;   // Engine power
    public float maxBrakeTorque = 3000f;   // Brake strength
    public float maxSteeringAngle = 15f;   // Steering angle
    public float maxSpeed = 180f;          // Max speed (km/h)
    public float downforce = 100f;         // Downforce amount

    [Header("Drift Settings")]
    public float normalStiffness = 1.5f;   // Normal grip
    public float driftStiffness = 0.5f;    // Drift grip (low friction)

    // Rigidbody reference
    private Rigidbody rb;

    // Rear wheel friction settings
    private WheelFrictionCurve rearFriction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Lower center of mass for better stability
        rb.centerOfMass = new Vector3(0, -0.5f, 0);

        // Initialize rear wheel friction
        rearFriction = rearLeftWheel.sidewaysFriction;
        rearFriction.stiffness = normalStiffness;
    }

    void FixedUpdate()
    {
        // ---------------- INPUT ----------------
        float motorInput = Input.GetAxis("Vertical");
        float steerInput = Input.GetAxis("Horizontal");
        bool isBraking = Input.GetKey(KeyCode.Space);
        bool isDrifting = Input.GetKey(KeyCode.LeftShift);

        // ---------------- SPEED CONTROL ----------------
        // Convert speed to km/h
        float speed = rb.linearVelocity.magnitude * 3.6f;

        if (speed < maxSpeed)
        {
            // Apply motor torque to rear wheels
            rearLeftWheel.motorTorque = motorInput * maxMotorTorque;
            rearRightWheel.motorTorque = motorInput * maxMotorTorque;
        }
        else
        {
            // Stop acceleration if max speed reached
            rearLeftWheel.motorTorque = 0;
            rearRightWheel.motorTorque = 0;
        }

        // ---------------- STEERING ----------------
        float steering = steerInput * maxSteeringAngle;

        // Reduce steering sensitivity at high speed
        if (speed > 100f)
            steering *= 0.6f;

        frontLeftWheel.steerAngle = steering;
        frontRightWheel.steerAngle = steering;

        // ---------------- BRAKING ----------------
        float brakeForce = isBraking ? maxBrakeTorque : 0f;

        frontLeftWheel.brakeTorque = brakeForce;
        frontRightWheel.brakeTorque = brakeForce;
        rearLeftWheel.brakeTorque = brakeForce;
        rearRightWheel.brakeTorque = brakeForce;

        // ---------------- DOWNFORCE ----------------
        // Apply downward force based on speed
        rb.AddForce(-transform.up * downforce * rb.linearVelocity.magnitude);

        // ---------------- DRIFT SYSTEM ----------------
        float stiffness = isDrifting ? driftStiffness : normalStiffness;
        ApplyDrift(stiffness);
    }

    void ApplyDrift(float stiffness)
    {
        // Adjust rear wheel sideways friction
        rearFriction.stiffness = stiffness;

        rearLeftWheel.sidewaysFriction = rearFriction;
        rearRightWheel.sidewaysFriction = rearFriction;
    }

    void LateUpdate()
    {
        // Update all wheel visuals
        UpdateWheel(frontLeftWheel, frontLeftVisual);
        UpdateWheel(frontRightWheel, frontRightVisual);
        UpdateWheel(rearLeftWheel, rearLeftVisual);
        UpdateWheel(rearRightWheel, rearRightVisual);
    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        // Sync collider with visual wheel model
        Vector3 pos;
        Quaternion rot;

        col.GetWorldPose(out pos, out rot);

        trans.position = pos;
        trans.rotation = rot;
    }
}