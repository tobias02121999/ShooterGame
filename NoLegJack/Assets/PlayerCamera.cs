using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Initialize the public variables
    public Transform playerTransform;
    public float speed;

    // Run this code every single frame
    void Update()
    {
        FollowPlayer(); // Follow the player
    }

    // Follow the player
    void FollowPlayer()
    {
        Vector3 target = playerTransform.position;
        target.z = transform.position.z;

        transform.position = Vector3.Slerp(transform.position, target, speed);
    }
}
