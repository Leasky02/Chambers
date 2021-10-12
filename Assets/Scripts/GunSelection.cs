using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelection : MonoBehaviour
{
    //variable holding each gun
    [SerializeField] private GameObject Pistol;
    [SerializeField] private GameObject Rifle;

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
        if(Input.GetKeyDown(KeyCode.Alpha1) && Pistol.activeInHierarchy == false)
        {
            //set rifle to active and stop it from zooming out
            Pistol.SetActive(true);
            Pistol.GetComponent<Gun>().isZoomed = false;
            Pistol.GetComponent<Gun>().zoomOut = false;
            Rifle.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && Rifle.activeInHierarchy == false)
        {
            //set rifle to active and stop it from zooming out
            Rifle.SetActive(true);
            Rifle.GetComponent<Gun>().isZoomed = false;
            Rifle.GetComponent<Gun>().zoomOut = false;
            Pistol.SetActive(false);
        }
    }
}
