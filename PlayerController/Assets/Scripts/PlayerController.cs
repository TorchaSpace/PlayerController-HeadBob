using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float smoothTime; // The smooth time for movement
    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude > 0f)
        {
            Vector3 targetVelocity = direction * speed * Time.deltaTime;
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);
        }
        else
        {
            rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector3.zero, ref velocity, smoothTime);
        }

        transform.position = Vector3.Lerp(transform.position, rb.position, Time.fixedDeltaTime * smoothTime);
    }
}
