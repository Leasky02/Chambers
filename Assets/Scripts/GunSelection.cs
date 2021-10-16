using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelection : MonoBehaviour
{
    //variable holding each gun
    [SerializeField] private GameObject Pistol;
    [SerializeField] private GameObject Rifle;
    //variable for what gun is selected (0 is pistol, 1 is gun)
    [SerializeField] private int gunSelected = 0;

    [SerializeField] private Camera fpsCamera;
    // Start is called before the first frame update
    void Start()
    {
        //default start with pistol
        Pistol.SetActive(true);
        Rifle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if switch weapon button is pressed
        if (Input.GetButtonDown("SwitchWeapon"))
        {
            //if any gun is reloading
            if(!Pistol.GetComponent<Gun>().reloading && !Rifle.GetComponent<Gun>().reloading)
            {
                //add 1 to menu and loop round if > 1
                gunSelected++;
                if (gunSelected > 1)
                    gunSelected = 0;
                //equip weapon
                EquipWeapon();
            }
        }
    }

    private void EquipWeapon()
    {
        if (gunSelected == 0 && Pistol.activeInHierarchy == false)
        {
            //set rifle to active and stop it from zooming out
            Pistol.SetActive(true);
            Pistol.GetComponent<Gun>().isZoomed = false;
            Pistol.GetComponent<Gun>().zoomOut = false;
            Rifle.SetActive(false);
        }
        if (gunSelected == 1 && Rifle.activeInHierarchy == false)
        {
            //set rifle to active and stop it from zooming out
            Rifle.SetActive(true);
            Rifle.GetComponent<Gun>().isZoomed = false;
            Rifle.GetComponent<Gun>().zoomOut = false;
            Pistol.SetActive(false);
        }
    }

}
