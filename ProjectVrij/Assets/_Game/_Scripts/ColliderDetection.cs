using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    // Initialize the public variables
    [HideInInspector]
    public bool isColliding;

    // Run this code on collision trigger enter
    void OnTriggerEnter(UnityEngine.Collider other)
    {
        // Tell the script the object is currently colliding
        isColliding = true;
    }

    // Run this code on collision trigger exit
    void OnTriggerExit(UnityEngine.Collider other)
    {
        // Tell the script the object is currently colliding
        isColliding = false;
    }
}
