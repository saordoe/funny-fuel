using UnityEngine;

public class FollowCar : MonoBehaviour {
    public Transform target;

    public float height = 4f;
    public float distance = 7f;
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;

    // UPWARD tilt settings
    public float tiltMax = 35f;    // how far upward player can tilt
    public float tiltSpeed = 40f;  // mouse responsiveness

    private float tiltAmount = 0f; // start at normal camera angle

    private void LateUpdate() {
        if (!target) return;

        // FOLLOW car position
        Vector3 desiredPos = target.position
                             - target.forward * distance
                             + Vector3.up * height;

        transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);

        // -------------------------------
        // MOUSE UP = tilt camera upward
        // -------------------------------
        float mouseY = Input.GetAxis("Mouse Y");
        tiltAmount += mouseY * tiltSpeed * Time.deltaTime;
        tiltAmount = Mathf.Clamp(tiltAmount, 0f, tiltMax);  // only tilt upward

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
