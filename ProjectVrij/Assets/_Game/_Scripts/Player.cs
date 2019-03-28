using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Initialize the public enums
    public enum states { DEFAULT };

    // Initialize the public variables
    public states playerState;

    public GameObject playerCamera;
    public Animator playerGunAnimator;

    public float lookSensitivityX;
    public float lookSensitivityY;

    public float lookDampening;

    public float movementSpeed;
    public float jumpSpeed;

    public float reloadDuration;

    public float strafeCamIntensity;

    public ColliderDetection feetCollider;

    // Initialize the private variables
    float rotationX;
    float rotationY;

    bool isGrounded;
    bool isReloaded;

    float reloadAlarm;

    Rigidbody playerRigidbody;
    Transform playerCameraTransform;
    Animator playerCameraAnimator;

    // Run this code once at the start
    void Start()
    {
        // Get the rigidbody component from the player
        playerRigidbody = GetComponent<Rigidbody>();

        // Get the components from the player camera
        playerCameraTransform = playerCamera.GetComponent<Transform>();
        playerCameraAnimator = playerCamera.GetComponent<Animator>();
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
                StrafeCam();
                Animate();
                Shoot();
                Reload();
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
        playerCameraTransform.rotation = Quaternion.Slerp(playerCameraTransform.rotation, newCamRotation, step);
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

    // Effect of turning the camera sideways towards the strafing direction
    void StrafeCam()
    {
        float rotationZ = -Input.GetAxis("Horizontal") * strafeCamIntensity * Time.deltaTime;
        playerCamera.transform.rotation = Quaternion.Euler(playerCameraTransform.eulerAngles.x, playerCameraTransform.eulerAngles.y, rotationZ);
    }

    // Make the player shoot
    void Shoot()
    {
        if (Input.GetAxis("Fire1") != 0f && isReloaded)
            reloadAlarm = reloadDuration;
    }

    // Reload the players gun
    void Reload()
    {
        if (reloadAlarm > 0f)
        {
            isReloaded = false;
            reloadAlarm--;
        }
        else
        {
            if (Input.GetAxis("Fire1") == 0f)
                isReloaded = true;
        }
    }

    // Animate the player
    void Animate()
    {
        // Animate the player camera
        bool isMoving = ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && isGrounded);
        playerCameraAnimator.SetBool("isBobbing", isMoving);

        // Animate the player gun
        playerGunAnimator.SetBool("isShooting", !isReloaded);
    }
}
