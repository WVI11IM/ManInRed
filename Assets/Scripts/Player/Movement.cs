using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public Camera mainCamera;

    public bool canMove = true;
    bool onSolid = true;

    public Animator animator;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Vector3.down, out hit))
        {
            if (hit.collider.tag == "solid")
            {
                onSolid = true;
            }
            else if (hit.collider.tag == "grass")
            {
                onSolid = false;
            }
        }
    }


    void FixedUpdate()
    {
        MovementUpdate();
    }

    void MovementUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Calculate camera-relative movement
        Vector3 camForward = mainCamera.transform.forward;
        Vector3 camRight = mainCamera.transform.right;
        camForward.y = 0; //Ensure the movement stays on the XZ plane (optional)
        camRight.y = 0;
        Vector3 movement = (camForward * verticalInput + camRight * horizontalInput).normalized;

        //If time is paused or is being skipped, player cannot move
        if (TimeManager.Instance.timerIsPaused || TimeManager.Instance.skippingTime || PlayerStats.Instance.onMinigame) CanMove(false);
        else CanMove(true);

        if (canMove)
        {
            rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);

            //Rotate the player to face the movement direction
            if (movement != Vector3.zero)
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
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void CanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void StepSound()
    {
        float r = Random.Range(0f, 1f);
        if (onSolid)
        {
            if (r > 0.5f) AudioManager.Instance.PlaySoundEffect("stepInside");
            else AudioManager.Instance.PlaySoundEffect("stepInside2");
        }

        else
        {
            if (r > 0.5f) AudioManager.Instance.PlaySoundEffect("stepGrass");
            else AudioManager.Instance.PlaySoundEffect("stepGrass2");
        }
    }
}