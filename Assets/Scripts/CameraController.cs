using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody playerRb;          // drag Player's Rigidbody here
    public float ballRadius = 0.5f;     // set to your SphereCollider radius
    public float smoothTime = 0.10f;

    private Vector3 offset;
    private Vector3 velocity;
    private Vector3 stableTarget;       // updated in FixedUpdate (physics)

    void Start()
    {
        // Target point is the "top" of the ball (center + up*radius)
        stableTarget = playerRb.position + Vector3.up * ballRadius;
        offset = transform.position - stableTarget;
    }

    void FixedUpdate()
    {
        if (playerRb == null) return;
        stableTarget = playerRb.position + Vector3.up * ballRadius;
    }

    void LateUpdate()
    {
        if (playerRb == null) return;

        Vector3 desiredPos = stableTarget + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothTime);
    }
}