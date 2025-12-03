using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float turnSpeed = 120f;
    public float drag = 5f;
    public float modelYOffset = -90f;  // ADD THIS - adjust to 90 or -90

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        float forward = 0f;
        if (Input.GetKey(KeyCode.W))
            forward = 1f;
        else if (Input.GetKey(KeyCode.S))
            forward = -1f;

        // Get adjusted forward direction
        Vector3 adjustedForward = Quaternion.Euler(0, modelYOffset, 0) * transform.forward;

        // Apply forward movement with adjusted direction
        Vector3 velocity = adjustedForward * forward * moveSpeed;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        if (forward == 0f)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, drag * Time.fixedDeltaTime);
        }

        float turnInput = 0f;
        if (Input.GetKey(KeyCode.A)) turnInput = -1f;
        if (Input.GetKey(KeyCode.D)) turnInput = 1f;

        if (turnInput != 0f)
        {
            float turnAmount = turnInput * turnSpeed * Time.fixedDeltaTime;
            transform.Rotate(0f, turnAmount, 0f);
        }
    }
}