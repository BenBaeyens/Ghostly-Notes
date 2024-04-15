using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GhostFloating : MonoBehaviour
{
    public float floatSpeed = 2f; // Speed of the floating animation
    public float floatHeight = 1f; // Height of the floating animation

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position; 
        StartCoroutine(FloatingAnimation());
    }

    private IEnumerator FloatingAnimation()
    {
        while (true)
        {
            // Calculate the new Y position based on a sine wave
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            // Update the ghost's position
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null; // Wait for the next frame
        }
    }
}
