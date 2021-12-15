using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousesSensetivity : MonoBehaviour
{
    private void Start()
    {
        //set value to 100 by default when first played
        if(MoveLook.mouseSensetivity == 0)
        {
            MoveLook.mouseSensetivity = 200;
        }
        //set slider to alue of mouse sensetivity
        GetComponent<Slider>().value = MoveLook.mouseSensetivity;
    }
    // change value of mouse sensitivity
    public void ChangeSensetivity()
    {
        MoveLook.mouseSensetivity = GetComponent<Slider>().value;
    }
}
