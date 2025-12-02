using JetBrains.Annotations;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    private float jumpForce = 5f;
    public GameObject floor;
    public Camera cameraObject;

    private bool jumpRequest;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
            isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
            isGrounded = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequest = true;
        }

        Quaternion cameraQuat = cameraObject.transform.rotation;
        Vector3 cameraAngles = cameraQuat.eulerAngles;
        cameraAngles.x = 0;
        cameraAngles.z = 0;
        transform.eulerAngles = cameraAngles;
    }

    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;
        if (Input.GetKey("w"))
            movement.z += 20f * Time.deltaTime;

        if (Input.GetKey("a"))
            movement.x -= 20f * Time.deltaTime;

        if (Input.GetKey("s"))
            movement.z -= 20f * Time.deltaTime;

        if (Input.GetKey("d"))
            movement.x += 20f * Time.deltaTime;

        rb.MovePosition(movement + transform.position);

        if (jumpRequest && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        jumpRequest = false;
    }
}