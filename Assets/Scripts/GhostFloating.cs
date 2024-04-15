using UnityEngine;

public class GhostFloating : MonoBehaviour
{
    public float floatSpeed = 2f; // Speed of the floating animation
    public float floatHeight = 0.05f; // Height of the floating animation

    private void Update()
    {
        // Calculate the new position based on a sine wave along the transform.up direction
        transform.position += transform.up * Mathf.Sin(Time.time * floatSpeed) * floatHeight * Time.deltaTime;
    }
}