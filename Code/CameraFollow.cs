using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public Vector3 offset;    // Offset position of the camera relative to the player
    public float smoothSpeed = 0.125f; // How quickly the camera follows

    public Transform levelBounds;  // Reference to an object defining the level bounds (e.g., an empty GameObject with a BoxCollider2D)
    
    private float leftLimit;
    private float rightLimit;
    private float bottomLimit;
    private float topLimit;

    void Start()
    {
        // Get camera dimensions
        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        // Assuming the levelBounds has a BoxCollider2D that defines the level's boundaries
        if (levelBounds != null)
        {
            BoxCollider2D bounds = levelBounds.GetComponent<BoxCollider2D>();

            // Calculate camera limits based on the level size and camera dimensions
            leftLimit = bounds.bounds.min.x + camWidth;
            rightLimit = bounds.bounds.max.x - camWidth;
            bottomLimit = bounds.bounds.min.y + camHeight;
            topLimit = bounds.bounds.max.y - camHeight;
        }
    }

    void LateUpdate()
    {
        if (player != null)  // Check if the player is assigned
        {
            // Define the target position with the offset
            Vector3 targetPosition = player.position + offset;

            // Smoothly transition to the target position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // Clamp the smoothed position to the limits
            float clampedX = Mathf.Clamp(smoothedPosition.x, leftLimit, rightLimit);
            float clampedY = Mathf.Clamp(smoothedPosition.y, bottomLimit, topLimit);

            // Set the camera's position with the clamped values
            transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
        }
    }
}





