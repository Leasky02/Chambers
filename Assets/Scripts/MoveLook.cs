using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLook : MonoBehaviour
{
    //controls sensetivity of mouse
    public static float mouseSensetivity;
    //contains information of parent player object transform component
    public Transform playerBody;
    //contains how much camera should rotate each frame. 0f is default but will change
    float xRotation = 0f;
    //varieble if player is zoomed in or not
    [HideInInspector] public bool isZoomed = false;
    // Start is called before the first frame update
    void Start()
    {
        //locks cursor to game
        Cursor.lockState = CursorLockMode.Locked;

        //set value to 100 by default when first played
        if (MoveLook.mouseSensetivity == 0)
        {
            MoveLook.mouseSensetivity = 200;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isZoomed)
        {
            //create variables to hold the direction of each axis movement * mousesensetivity and time
            float mouseX = Input.GetAxis("LookX") * (mouseSensetivity / 2) * Time.deltaTime;
            float mouseY = Input.GetAxis("LookY") * (mouseSensetivity / 2) * Time.deltaTime;

            xRotation -= mouseY;
            //stops player from looking too far up or down
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            //rotates player according to local rotation using rotation given in that frame
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            //rotates the player
            playerBody.Rotate(Vector3.up * mouseX);
        }
        else
        {
            //create variables to hold the direction of each axis movement * mousesensetivity and time
            float mouseX = Input.GetAxis("LookX") * mouseSensetivity * Time.deltaTime;
            float mouseY = Input.GetAxis("LookY") * mouseSensetivity * Time.deltaTime;

            xRotation -= mouseY;
            //stops player from looking too far up or down
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            //rotates player according to local rotation using rotation given in that frame
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            //rotates the player
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void ZoomIn()
    {
        isZoomed = true;
    }
    public void ZoomOut()
    {
        isZoomed = false;
    }

}
