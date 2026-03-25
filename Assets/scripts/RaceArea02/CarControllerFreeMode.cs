using UnityEngine;

public class CarControllerFreeMode : MonoBehaviour
{
    // ---------------- WHEEL COLLIDERS ----------------
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    // ---------------- WHEEL VISUALS ----------------
    public Transform frontLeftVisual;
    public Transform frontRightVisual;
    public Transform rearLeftVisual;
    public Transform rearRightVisual;

    // Default friction (used to reset after drift)
    private WheelFrictionCurve defaultFriction;

    // ---------------- DRIFT SETTINGS ----------------
    public float driftStiffness = 0.4f;

    // ---------------- CAR MOVEMENT SETTINGS ----------------
    public float maxMotorTorque = 4000f;     // Acceleration power
    public float maxBrakeTorque = 1500f;     // Braking force
    public float maxSteeringAngle = 35f;     // Turning angle

    // Arcade feel enhancements
    public float turnBoost = 8f;             // Extra turning force
    public float downforce = 300f;           // Stability force

    // Rigidbody reference
    private Rigidbody rb;

    // ---------------- AUDIO SOURCES ----------------
    public AudioSource engineSound;  // Engine sound
    public AudioSource brakeSound;   // Brake sound
    public AudioSource driftSound;   // Drift sound

    void Start()
    {
        // Get Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Lower center of mass → improves stability
        rb.centerOfMass = new Vector3(0, -0.3f, 0);

        // Store default friction for reset after drift
        defaultFriction = rearLeftWheel.sidewaysFriction;
    }

    void FixedUpdate()
    {
        // ---------------- INPUT ----------------
        float motorInput = Input.GetAxis("Vertical");
        float steerInput = Input.GetAxis("Horizontal");

        bool isBraking = Input.GetKey(KeyCode.Space);
        bool isDrifting = Input.GetKey(KeyCode.LeftShift);

        // Get current speed
        float speed = rb.linearVelocity.magnitude;

        // ---------------- MOVEMENT ----------------

        // Apply motor torque to rear wheels
        rearLeftWheel.motorTorque = motorInput * maxMotorTorque;
        rearRightWheel.motorTorque = motorInput * maxMotorTorque;

        // Dynamic steering (reduce turning at high speed)
        float dynamicSteer = maxSteeringAngle;
        if (speed > 50f)
            dynamicSteer *= 0.7f;

        frontLeftWheel.steerAngle = steerInput * dynamicSteer;
        frontRightWheel.steerAngle = steerInput * dynamicSteer;

        // Apply braking force
        float brake = isBraking ? maxBrakeTorque : 0f;

        frontLeftWheel.brakeTorque = brake;
        frontRightWheel.brakeTorque = brake;
        rearLeftWheel.brakeTorque = brake;
        rearRightWheel.brakeTorque = brake;

        // ---------------- DRIFT SYSTEM ----------------
        if (isDrifting)
        {
            SetWheelStiffness(rearLeftWheel, driftStiffness);
            SetWheelStiffness(rearRightWheel, driftStiffness);

            rb.AddTorque(Vector3.up * steerInput * 20f);

            // 🔊 Smooth Drift Sound ON
            driftSound.volume = Mathf.Lerp(driftSound.volume, 1f, Time.deltaTime * 5f);

            if (!driftSound.isPlaying)
                driftSound.Play();
        }
        else
        {
            rearLeftWheel.sidewaysFriction = defaultFriction;
            rearRightWheel.sidewaysFriction = defaultFriction;

            // 🔊 Smooth Drift Sound OFF
            driftSound.volume = Mathf.Lerp(driftSound.volume, 0f, Time.deltaTime * 5f);

            // Fully stop when silent
            if (driftSound.volume < 0.05f)
                driftSound.Stop();
        }

        // ---------------- ENGINE SOUND ----------------
        if (Mathf.Abs(motorInput) > 0.1f)
        {
            // Play engine sound if moving
            if (!engineSound.isPlaying)
                engineSound.Play();

            // Adjust pitch based on speed
            engineSound.pitch = 0.5f + speed * 0.02f;
        }
        else
        {
            // Idle engine sound
            engineSound.pitch = 0.5f;
        }

        // ---------------- BRAKE SOUND ----------------
        if (isBraking && speed > 5f)
        {
            // Play brake sound only when moving
            if (!brakeSound.isPlaying)
                brakeSound.Play();
        }
        else
        {
            // Stop brake sound
            brakeSound.Stop();
        }

        // ---------------- PHYSICS ENHANCEMENT ----------------

        // Apply downward force for better grip
        rb.AddForce(-transform.up * downforce);

        // Arcade-style turning boost
        rb.AddTorque(Vector3.up * steerInput * speed * turnBoost);
    }

    // Function to modify wheel friction (for drifting)
    void SetWheelStiffness(WheelCollider wheel, float stiffness)
    {
        WheelFrictionCurve friction = wheel.sidewaysFriction;
        friction.stiffness = stiffness;
        wheel.sidewaysFriction = friction;
    }

    void LateUpdate()
    {
        // Update all wheel visuals every frame
        UpdateWheel(frontLeftWheel, frontLeftVisual);
        UpdateWheel(frontRightWheel, frontRightVisual);
        UpdateWheel(rearLeftWheel, rearLeftVisual);
        UpdateWheel(rearRightWheel, rearRightVisual);
    }

    // Sync collider with visual model
    void UpdateWheel(WheelCollider col, Transform mesh)
    {
        Vector3 pos;
        Quaternion rot;

        col.GetWorldPose(out pos, out rot);

        mesh.position = pos;
        mesh.rotation = rot;
    }
}