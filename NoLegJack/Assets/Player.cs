using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Initialize the public enums
    public enum states { DEFAULT };

    // Initialize the public variables
    public states playerState;
    public Transform gunSpriteTransform;
    public float gunForce;
    public float reloadDuration;

    // Initialize the private variables
    Rigidbody2D playerRigidbody;
    float reloadAlarm;
    bool isReloaded;

    // Run this code once at the start
    void Start()
    {
        // Get the player rigidbody 2D component
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Run this code every single (fixed) frame
    void FixedUpdate()
    {
        RunState(); // Run the current player state
    }

    // Run the current player state
    void RunState()
    {
        // Switch through the player states
        switch (playerState)
        {
            // The default player state
            case states.DEFAULT:
                Aim(); // Aim the gun at the mouse position
                Shoot(); // Shoot the gun
                Reload(); // Reload the gun
                break;
        }
    }

    // Aim the gun at the mouse position
    void Aim()
    {
        // Get the mouse position within the world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Get the direction between the gun and the mouse
        Vector2 direction = new Vector2(mousePosition.x - gunSpriteTransform.position.x, mousePosition.y - gunSpriteTransform.position.y);

        // Apply the direction to the gun transform
        gunSpriteTransform.right = direction;
    }

    // Shoot the gun
    void Shoot()
    {
        // Check for user shoot input
        if (Input.GetAxis("Fire1") != 0f && isReloaded)
        {
            playerRigidbody.AddForce(-gunSpriteTransform.right * gunForce);
            reloadAlarm = reloadDuration;
        }
    }

    // Reload the gun
    void Reload()
    {
        if (reloadAlarm > 0f)
        {
            reloadAlarm--;
            isReloaded = false;
        }
        else
        {
            if (Input.GetAxis("Fire1") == 0f)
                isReloaded = true;
        }
    }
}
