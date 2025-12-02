using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset; // for camera offset

    private void Start()
    {
        offset = new Vector3(0f, 50f, -150f) * Time.deltaTime;
    }
    void Update()
    {
        transform.position = player.position + offset;
        //transform.rotation = player.rotation + offsetRotation;
    }
}
