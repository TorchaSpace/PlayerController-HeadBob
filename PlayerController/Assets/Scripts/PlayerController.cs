using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; // The speed of the player
    public float smoothTime; // The smooth time for movement
    private Vector3 velocity = Vector3.zero; // The current velocity of the player
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // Get the horizontal input
        float vertical = Input.GetAxisRaw("Vertical"); // Get the vertical input

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // Create a direction vector from the input

        if(direction.magnitude > 0f) // Check if player is moving
        {
            Vector3 targetVelocity = direction * speed * Time.deltaTime; // Calculate the target velocity
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime); // Smoothly change the player's velocity
        }
        else // If player is not moving, smootlhy stop them
        {
            rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector3.zero, ref velocity, smoothTime); // smootlhy stop the player
        }

        // Interpolate the player's position to make movement smoother
        transform.position = Vector3.Lerp(transform.position, rb.position, Time.fixedDeltaTime * smoothTime);

    }
}
