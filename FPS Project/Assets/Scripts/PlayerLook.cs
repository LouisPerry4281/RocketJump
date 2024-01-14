using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("References")]
    [SerializeField] WallRun wallRun;

    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;

    [SerializeField] Transform cam = null;
    [SerializeField] Transform orientation = null;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    private void Start()
    {
        //Lock cursor and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Get the raw mouse input
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
         
        //Applies the sensitivity and normalisation multiplier
        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        //Clamps the x rotation so the player can't break their neck
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Sets the determined rotation with the tilt from the wall run (if wallrunning)
        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, wallRun.tilt);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
