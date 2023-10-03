using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public Camera mainCamera;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate camera-relative movement
        Vector3 camForward = mainCamera.transform.forward;
        Vector3 camRight = mainCamera.transform.right;
        camForward.y = 0; // Ensure the movement stays on the XZ plane (optional)
        camRight.y = 0;

        Vector3 movement = (camForward * verticalInput + camRight * horizontalInput).normalized;

        // Check if there's no input (all inputs are zero) and stop the movement
        if (Mathf.Approximately(horizontalInput, 0f) && Mathf.Approximately(verticalInput, 0f))
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            // Move the player with Time.fixedDeltaTime (use FixedUpdate's delta time)
            rb.velocity = movement * moveSpeed * Time.fixedDeltaTime;

            // Rotate the player to face the movement direction
            if (movement != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(movement, Vector3.up);
                rb.rotation = newRotation;
            }
        }

    }
}