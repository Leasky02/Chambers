using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDataManager : MonoBehaviour
{
    //variable holding the ammo prefab
    [SerializeField] private GameObject ammoPrefab;
    //ammo total
    [HideInInspector] public int ammo;
    //limits of ammo spawning
    [SerializeField] private int[] ammoBounds;
    //variable holding the string of the ammo type
    public string ammoType;

    // Start is called before the first frame update
    void Start()
    {
        //set ammo amount randomly between bounds
        ammo = Random.Range(ammoBounds[0], ammoBounds[1]);
        //Debug.Log(ammo);
        //loop through ammo and spawn
        for (int i = 0; i < ammo; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f),
            transform.position.y, Random.Range(transform.position.z - 0.5f, transform.position.z + 0.5f));
            //create the prefab
            GameObject currentPrefab = Instantiate(ammoPrefab, randomPosition, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            //set the prefab as a child to the gameObject
            currentPrefab.transform.parent = gameObject.transform;
        }
        //disable movement and collision for ammo
        Invoke("DisablComponents", 3f);
    }

    //method to disable components after being settled into place and enable the box collider of the ammo parent (self)
    public void DisablComponents()
    {
        //disable movement for all ammo children
        for (int i = 0; i < ammo; i++)
        {
            //sets the rigidbody to kinematic
            transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = true;
            //disable the box collider
            transform.GetChild(i).GetComponent<CapsuleCollider>().enabled = false;
        }
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
