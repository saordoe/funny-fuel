using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    private float xRotation;
    private float yRotation;
    private float sensitivity = 50f;

    // we essentially want to create a Quaternion var to reference the Camera's quaternion and then a
    // Vector3 to represent the eulerangles of the quaternion var
    // 1) modify Vector3 to match the transform of the player
    // 2) set the quaternion's eulerangles to the Vector3 
    // 3) set transform.rotation  to the quaternion

    // Update is called once per frame
    void Update()
    {
        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        yRotation += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -30f, 100f);
        transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);
        //desiredEulerAngle = player.rotation.eulerAngles;
        //cameraRotation.eulerAngles = desiredEulerAngle;
        //transform.rotation = cameraRotation;
    }
}
