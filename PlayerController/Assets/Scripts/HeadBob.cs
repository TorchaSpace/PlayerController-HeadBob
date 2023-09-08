using UnityEngine;
using System.Collections;

public class HeadBob : MonoBehaviour
{
    // Set the maximum amount of movement in each direction
    public float bobAmountX = 0.1f;
    public float bobAmountY = 0.05f;

    // Set the speed of the head bobbing
    public float bobSpeed = 0.2f;

    // Set the maximum amount of rotation in each direction
    public float rotationAmountX = 5f;
    public float rotationAmountY = 3f;
    public float rotationAmountZ = 3f;

    // Set the speed of the head rotation
    public float rotationSpeed = 0.2f;

    // Keep track of the current position and rotation
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Keep track of the current target position and rotation
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    // Keep track of whether the head bob is active
    private bool bobbing = false;

    // Initialize the starting position and rotation
    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
        targetPosition = initialPosition;
        targetRotation = initialRotation;
    }

    // Update the head bob and rotation if bobbing is active
    void Update()
    {
        if (PlayerController.isMoving)
        {
            bobbing = true;
        }
        else
        {
            bobbing = false;
        }

        // Calculate the current bobbing amount using the PingPong function
        float currentBobAmount = Mathf.PingPong(Time.time * bobSpeed, 1f);

        if (bobbing)
        {
            // Calculate the new position and rotation targets
            float posX = Mathf.Sin(currentBobAmount * Mathf.PI * 2f) * bobAmountX;
            float posY = Mathf.Cos(currentBobAmount * Mathf.PI * 2f * 2f) * bobAmountY;
            float rotX = Mathf.Sin(currentBobAmount * Mathf.PI * 2f) * rotationAmountX;
            float rotY = Mathf.Cos(currentBobAmount * Mathf.PI * 2f * 2f) * rotationAmountY;
            float rotZ = Mathf.Cos(currentBobAmount * Mathf.PI * 2f) * rotationAmountZ;
            Vector3 newPosition = initialPosition + new Vector3(posX, posY, 0f);
            Quaternion newRotation = initialRotation * Quaternion.Euler(rotX, rotY, rotZ);

            // Smoothly transition to the new position and rotation targets
            targetPosition = Vector3.Lerp(targetPosition, newPosition, Time.deltaTime * 5f);
            targetRotation = Quaternion.Lerp(targetRotation, newRotation, Time.deltaTime * 5f);
        }
        else
        {
            // Calculate the new position and rotation targets
            Vector3 newPosition = initialPosition;
            Quaternion newRotation = initialRotation;

            // Smoothly transition to the new position and rotation targets
            targetPosition = Vector3.Lerp(targetPosition, newPosition, Time.deltaTime * 5f);
            targetRotation = Quaternion.Lerp(targetRotation, newRotation, Time.deltaTime * 5f);
        }

        // Apply the new position and rotation to the object
        transform.localPosition = targetPosition;
        transform.localRotation = targetRotation;
    }
}
