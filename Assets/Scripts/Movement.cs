using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public Camera mainCamera;

    bool canMove = true;

    Animator animator;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate camera-relative movement
        Vector3 camForward = mainCamera.transform.forward;
        Vector3 camRight = mainCamera.transform.right;
        camForward.y = 0; // Ensure the movement stays on the XZ plane (optional)
        camRight.y = 0;

        Vector3 movement = (camForward * verticalInput + camRight * horizontalInput).normalized;

        // Move the player with Time.deltaTime (use Update's delta time)
        if (canMove) rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);

        // Rotate the player to face the movement direction
        if (movement != Vector3.zero && canMove)
        {
            Quaternion newRotation = Quaternion.LookRotation(movement, Vector3.up);
            rb.rotation = newRotation;
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void CanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}