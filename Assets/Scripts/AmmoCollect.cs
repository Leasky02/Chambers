using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollect : MonoBehaviour
{
    //gun reference variables
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject rifle;

    //object that plays ammo sound
    [SerializeField] private GameObject ammoSoundPlayer;

    //when player collides
    private void OnTriggerEnter(Collider other)
    {
        //if the object collided with is tagged with "ammo"
        if (other.gameObject.CompareTag("ammo"))
        {
            Debug.Log("collisionx2");
            //then if the ammo type is for rifle
            if (other.gameObject.GetComponent<AmmoDataManager>().ammoType == "Rifle")
            {
                //add the ammo to the total ammo
                rifle.GetComponent<Gun>().totalAmmo += other.gameObject.GetComponent<AmmoDataManager>().ammo;
            }
            //if ammo type is for pistol
            if (other.gameObject.GetComponent<AmmoDataManager>().ammoType == "Pistol")
            {
                //add the ammo to the total ammo
                pistol.GetComponent<Gun>().totalAmmo += other.gameObject.GetComponent<AmmoDataManager>().ammo;
            }
            //play ammo collect sound
            ammoSoundPlayer.GetComponent<AudioSource>().Play();
            //destroy ammo object
            Destroy(other.gameObject);
        }
    }
}
