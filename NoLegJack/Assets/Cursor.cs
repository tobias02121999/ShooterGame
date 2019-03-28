using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    // Initialize the public variables
    public float sensitivity = 1f;

    // Run this code once at the start
    void Start()
    {
        //Screen.lockCursor = true;
    }

    // Run this code every single frame
    void Update()
    {
        Move(); // Move the cursor around
    }

    // Move the cursor around
    void Move()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 cursorPosition = mousePosition;
        cursorPosition.z = 0f;

        transform.position = cursorPosition;
    }
}
