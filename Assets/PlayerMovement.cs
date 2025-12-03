using UnityEngine;

// Arcade style car controller
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 20f;          // forward speed
    public float turnSpeed = 120f;         // how fast A/D rotate
    public float drag = 5f;                // how fast it stops when not pressing anything

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;   // avoid physics tipping over
    }

    void FixedUpdate() {
        float forward = 0f;

        if (Input.GetKey(KeyCode.W))
            forward = 1f;
        else if (Input.GetKey(KeyCode.S))
            forward = -1f;

        // Apply forward movement
        Vector3 velocity = transform.forward * forward * moveSpeed;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        // Apply drag (so it stops quickly when no input)
        if (forward == 0f) {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, drag * Time.fixedDeltaTime);
        }

        // A/D turning (always responsive)
        float turnInput = 0f;

        if (Input.GetKey(KeyCode.A)) turnInput = -1f;
        if (Input.GetKey(KeyCode.D)) turnInput = 1f;

        // Rotate even if not moving
        if (turnInput != 0f) {
            float turnAmount = turnInput * turnSpeed * Time.fixedDeltaTime;
            transform.Rotate(0f, turnAmount, 0f);
        }
    }
}
