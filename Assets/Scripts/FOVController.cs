using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVController : MonoBehaviour
{
    //camera to change FOV of
    [SerializeField] private Camera fpsCam;

    //defines if camera should zoom out
    public bool isZoomed = false;

    private bool isSlowSensetivity = true;

    [SerializeField] private float FOV = 40f;

    // Update is called once per frame
    void Update()
    {
        //if camera should be zoomed in
        if (isZoomed)
        {
            //then if the player is sprinting
            if (gameObject.GetComponent<PlayerMovement>().sprinting)
            {
                //Zoom to FOV + 20
                fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, FOV + 20, Time.deltaTime * 7f);
            }
            else
            {
                //if so, zoom to FOV
                fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, FOV, Time.deltaTime * 7);
            }

            //slow down player speed
            gameObject.GetComponent<PlayerMovement>().speed = 3.5f;
            //lower mouse sensetivity
            if(!isSlowSensetivity)
            {
                //decrease mouse sensetivity
                fpsCam.GetComponent<MoveLook>().ZoomIn();
                //allows it to speed up sensetivity
                isSlowSensetivity = true;
            }
        }
        //if camera should be zoomed out
        if (!isZoomed)
        {
            //then if player is sprinting
            if (gameObject.GetComponent<PlayerMovement>().sprinting)
            {
                //zoom to normal + 20
                fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, 80, Time.deltaTime * 7);
            }
            else
            {
                //if not, zoom to normal
                fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, 60, Time.deltaTime * 7);
            }

            //speed up player speed
            gameObject.GetComponent<PlayerMovement>().speed = 7f;
            if(isSlowSensetivity)
            {
                //increase mouse sensetivity
                fpsCam.GetComponent<MoveLook>().ZoomOut();
                //allows it to slow down sensetivity
                isSlowSensetivity = false;
            }
        }
    }
}
