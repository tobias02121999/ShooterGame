using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Initialize the public enums
    public enum states { DEFAULT };

    // Initialize the public variables
    public states playerState;

    public Transform playerCamera;
    public Transform gun;

    public float lookSensitivityX;
    public float lookSensitivityY;

    public float lookDampening;

    public float movementSpeed;
    public float jumpSpeed;

    public ColliderDetection feetCollider;

    // Initialize the private variables
    float rotationX;
    float rotationY;

    bool isGrounded;

    Rigidbody playerRigidbody;

    // Run this code once at the start
    void Start()
    {
        // Get the rigidbody component from the player
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Run this code every single frame
    void FixedUpdate()
    {
        // Run the player functions according to the current state   
        switch (playerState)
        {
            // The default player state
            case states.DEFAULT:
                Turn();
                Move();
                Jump();
                CheckGround();
                break;
        }
    }

    // Turn the player to face the mouse
    void Turn()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSensitivityY * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse X") * lookSensitivityX * Time.deltaTime;

        var newPlayerRotation = Quaternion.Euler(transform.eulerAngles.x, rotationY, transform.eulerAngles.z);
        var newCamRotation = Quaternion.Euler(rotationX, transform.eulerAngles.y, transform.eulerAngles.z);

        float step = lookDampening * Time.deltaTime;

        transform.rotation = Quaternion.Slerp(transform.rotation, newPlayerRotation, step);
        playerCamera.rotation = Quaternion.Slerp(playerCamera.rotation, newCamRotation, step);
    }

    // Move the player around
    void Move()
    {
        Vector3 newVelocity = ((transform.forward * Input.GetAxis("Vertical") * movementSpeed) + (transform.right * Input.GetAxis("Horizontal") * movementSpeed)) * Time.deltaTime;
        newVelocity.y = playerRigidbody.velocity.y;

        playerRigidbody.velocity = newVelocity;
    }

    // Make the player jump
    void Jump()
    {
        if (Input.GetAxis("Jump") > 0f && isGrounded)
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpSpeed, playerRigidbody.velocity.z);
    }

    // Check if the player is currently grounded
    void CheckGround()
    {
        isGrounded = (feetCollider.isColliding);
    }
}
