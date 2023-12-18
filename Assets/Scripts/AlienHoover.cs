using UnityEngine;

public class AlienHoover : MonoBehaviour
{
    public float hoverAmount = 5.0f; // How high the object moves up and down
    public float hoverSpeed = 4.0f;  // How fast the object hovers

    private float originalY;

    void Start()
    {
        // Store the original Y position of the GameObject
        originalY = transform.position.y;
    }

    void Update()
    {
        // Calculate a new Y position using a sine wave, adjusting for speed and amount
        float newY = originalY + hoverAmount * Mathf.Sin(Time.time * hoverSpeed);

        // Apply the new position to the GameObject
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
