using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //variable holding the artifactValueDisplay
    private GameObject artifactValueDisplay;

    //artifact transform variable
    private Transform artifactPosition;

    //rigidbody of gameobject
    private Rigidbody rb;
    //collider of gameObject
    private Collider coll;

    //colours
    private Color blue = new Color(123f / 255f, 157f / 255f, 230f / 255f, 1f);
    private Color orange = new Color(230f / 255f, 107f / 255f, 21f / 255f, 1f);
    private Color purple = new Color(230f / 255f, 64f / 255f, 227f / 255f, 1f);

    private void Start()
    {
        slotFull = false;

        //assign all objects to variables
        fpsCam = GameObject.Find("Main Camera").transform;
        player = GameObject.Find("Player");
        guns[0] = fpsCam.GetChild(1).gameObject;
        guns[1] = fpsCam.GetChild(2).gameObject;
        artifactContainer = GameObject.Find("Artifact Container");
        artifactValueDisplay = GameObject.Find("ValueDisplay");
        


        //set rigidbody to gameobject
        rb = gameObject.GetComponent<Rigidbody>();
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
        if (Input.GetButtonDown("PickUp") && !slotFull && distanceToPlayer.magnitude < pickupRange && !equipped)
        {
            //do pickup function
            PickUp();
        }
        else if (Input.GetButtonDown("PickUp") && slotFull && equipped)
        {
            //do drop function
            Drop();
        }
    }
    //pick up item
    void PickUp()
    {
        if(!slotFull)
        {
            slotFull = true;
            equipped = true;

            //set layer to "Weapon" (7)
            //this will stop it clipping into walls
            transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Weapon");

            //make the artifact a child of the artifact container
            transform.SetParent(artifactContainer.transform);
            //set all positions to 0
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);

            //Make Rigidbody kinematic and boxcollider a trigger
            rb.isKinematic = true;

            //disable gun script
            player.GetComponent<GunSelection>().enabled = false;

            //set additional weight multiplier of player
            player.GetComponent<PlayerMovement>().additionalWeight = true;

            //display the value to Player
            artifactValueDisplay.GetComponent<Text>().text = ("Value: £" + gameObject.GetComponent<ArtifactData>().value);
            
            //sset colour of text depending on what class the artifact is
            if(gameObject.GetComponent<ArtifactData>().artifactClass == 1)
            {
                //set colour to blue
                artifactValueDisplay.GetComponent<Text>().color = blue;
            }
            else if(gameObject.GetComponent<ArtifactData>().artifactClass == 2)
            {
                //set colour to orange
                artifactValueDisplay.GetComponent<Text>().color = orange;
            }
            else
            {
                //set colour to purple
                artifactValueDisplay.GetComponent<Text>().color = purple;
            }

            //if gun 0 is active in the hierarchy...
            if (guns[0].activeInHierarchy == true)
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
    }

    void Drop()
    {
        if(slotFull)
        {
            slotFull = false;
            equipped = false;

            //set layer to "Default" (0)
            //this will stop it clipping into walls
            transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");

            //set parent to null
            transform.SetParent(null);

            //Make Rigidbody NOT kinematic and boxcollider NOT a trigger
            rb.isKinematic = false;

            //object carries momentum of the player
            rb.velocity = player.GetComponent<PlayerMovement>().velocity;

            //add force to object to throw away
            rb.AddForce(player.transform.forward * dropForwardForce, ForceMode.Impulse);
            rb.AddForce(player.transform.up * dropUpwardForce, ForceMode.Impulse);

            //disable gun script
            player.GetComponent<GunSelection>().enabled = true;

            //set additional weight multiplier of player
            player.GetComponent<PlayerMovement>().additionalWeight = false;

            //display the value to Player
            artifactValueDisplay.GetComponent<Text>().text = ("Value: £0");
            //set text to white
            artifactValueDisplay.GetComponent<Text>().color = new Color(1f, 1f, 1f);

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
}
