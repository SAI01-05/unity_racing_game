using UnityEngine;

public class FinishRotate : MonoBehaviour
{
    // Target (usually player car) around which camera rotates
    public Transform target;

    void Update()
    {
        // Rotate camera around the target on Y-axis
        transform.RotateAround(
            target.position,
            Vector3.up,
            20 * Time.deltaTime
        );

        // Always look at the target
        transform.LookAt(target);
    }
}