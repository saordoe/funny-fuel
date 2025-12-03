using UnityEngine;
public class FollowCar : MonoBehaviour
{
    public Transform target;
    public float height = 4f;
    public float distance = 7f;
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;

    // Add this to compensate for model orientation
    public float carModelYOffset = 180f; // adjust this value as needed (90, -90, 180, etc.)

    public float tiltMax = 35f;
    public float tiltSpeed = 40f;
    private float tiltAmount = 0f;

    private void LateUpdate()
    {
        if (!target) return;

        // Get the car's adjusted forward direction
        Vector3 adjustedForward = Quaternion.Euler(0, carModelYOffset, 0) * target.forward;

        // FOLLOW car position using adjusted forward
        Vector3 desiredPos = target.position
                             + adjustedForward * distance
                             + Vector3.up * height;
        transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);

        // MOUSE UP = tilt camera upward
        float mouseY = Input.GetAxis("Mouse Y");
        tiltAmount += mouseY * tiltSpeed * Time.deltaTime;
        tiltAmount = Mathf.Clamp(tiltAmount, 0f, tiltMax);

        // BASE ROTATION = look at car normally
        Quaternion baseRot = Quaternion.LookRotation(target.position - transform.position);

        // EXTRA upward tilt around local X axis
        Quaternion tiltRot = Quaternion.Euler(-tiltAmount, 0f, 0f);

        // FINAL camera rotation
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            baseRot * tiltRot,
            rotationSpeed * Time.deltaTime
        );
    }
}