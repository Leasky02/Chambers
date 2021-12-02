using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public bool check;
    //variable to hold if player is holding an item
    private static bool slotFull = false;
    //variable containing if THIS gameObject is already equipped
    private bool equipped = false;

    //variable to hold how far away an item can be to be picked up
    [SerializeField] private float pickupRange, dropForwardForce, dropUpwardForce;

    //gun object
    [SerializeField] private Transform fpsCam;

    //player transform variable
    [SerializeField]private GameObject player;
    //gun objects
    [SerializeField] private GameObject[] guns;
    //variable containing what gun was previously active
    private int activeGun = 0;

    //variable holding the artifact container
    [SerializeField] private GameObject artifactContainer;

    //artifact transform variable
    private Transform artifactPosition;

    //rigidbody of gameobject
    private Rigidbody rb;
    //collider of gameObject
    private Collider coll;

    private void Awake()
    {
        //assign all objects to variables
        fpsCam = GameObject.Find("Main Camera").transform;
        player = GameObject.Find("Player");
        guns[0] = GameObject.Find("Handgun_01");
        guns[1] = GameObject.Find("Assault_Rifle_01");
        artifactContainer = GameObject.Find("Artifact Container");
        


        //set collider to gameobject
        rb = gameObject.GetComponent<Rigidbody>();
        //set rigidbody to gameobject
        coll = gameObject.transform.GetChild(0).GetComponent<BoxCollider>();
        //get transform of self
        artifactPosition = gameObject.GetComponent<Transform>();
    }
    //called every frame
    private void Update()
    {
        //get distance to player
        Vector3 distanceToPlayer = player.GetComponent<Transform>().position - transform.position;
        //if player is putting pickup input & doesn't already have an item & The artifact is within range
        // and if this object is already equipped
        if (Input.GetKeyDown(KeyCode.E) && !slotFull && distanceToPlayer.magnitude < pickupRange && !equipped)
        {
            //do pickup function
            PickUp();
        }
        else if (Input.GetKeyDown(KeyCode.E) && slotFull && equipped)
        {
            //do drop function
            Drop();
        }

    }
    //pick up item
    void PickUp()
    {
        slotFull = true;
        equipped = true;

        //set layer to "Weapon" (7)
        //this will stop it clipping into walls
        gameObject.layer = LayerMask.NameToLayer("Weapon");

        //make the artifact a child of the artifact container
        transform.SetParent(artifactContainer.transform);
        //set all positions to 0
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;


        //Make Rigidbody kinematic and boxcollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //disable gun script
        player.GetComponent<GunSelection>().enabled = false;

        //set additional weight multiplier of player
        player.GetComponent<PlayerMovement>().additionalWeight = gameObject.GetComponent<ArtifactData>().weightSpeedMultiplier;

        //if gun 0 is active in the hierarchy...
        if(guns[0].activeInHierarchy == true)
        {
            activeGun = 0;
        }
        else
        {
            activeGun = 1;
        }

        //disable gun object
        guns[0].SetActive(false);
        guns[1].SetActive(false);
    }

    void Drop()
    {
        slotFull = false;
        equipped = false;

        //set layer to "Default" (0)
        //this will stop it clipping into walls
        gameObject.layer = LayerMask.NameToLayer("Default");

        //set parent to null
        transform.SetParent(null);

        //Make Rigidbody NOT kinematic and boxcollider NOT a trigger
        rb.isKinematic = false;
        coll.isTrigger = false;

        //object carries momentum of the player
        rb.velocity = player.GetComponent<PlayerMovement>().velocity;

        //add force to object to throw away
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        //disable gun script
        player.GetComponent<GunSelection>().enabled = true;

        //set additional weight multiplier of player
        player.GetComponent<PlayerMovement>().additionalWeight = 1f;

        //enable gun object
        if (activeGun == 0)
        {
            guns[0].SetActive(true);
        }
        else
        {
            guns[1].SetActive(true);
        }
    }
}
