using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorwayRandomiser : MonoBehaviour
{
    //bool variable to contain if door should potentially close
    [SerializeField] private bool attemptClose;
    //variable to hold the value of closing
    private float closeChance;
    //variable holding the required chance of closing (maximum 10) with higher the number, the more likely it will close
    private static float requiredChance = 3.0f;
    //variable containing if the door is open
    private bool openState = false;

    //variable to hold the animator component
    private Animator myAnimator;
    //variable string holding number of door
    [SerializeField] private string doorNumber;

    //TEMP testing variable to summon an object to visualise what doors can potentially close
    [SerializeField] private GameObject visualIdentifierPrefab;
    //TEMP variable to contain the instantiated identifier
    private GameObject visualIdentifierObject;
    //TEMP variable to contain materials for object visualisation
    [SerializeField] private Material[] visualMaterial;

    private void Awake()
    {
        //set animator variable to the component
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (attemptClose)
        {
            //TEMP Create position from object position
            Vector3 position = GetComponent<Transform>().position;
            //TEMP set y position up higher above the level
            position.y = position.y + 20;
            //TEMP create object with position above
            visualIdentifierObject = Instantiate(visualIdentifierPrefab, position, Quaternion.identity);

            //do the wall change function in 1 seconds
            Invoke("ChangeWall", 1f);
        }
        //if the door is visible to begin with (a wall is there all the time)
        if(GetComponent<MeshRenderer>().enabled)
        {
            //play the door closed animation
            myAnimator.Play("Door" + doorNumber + " Closed");
            //set door to not open
            openState = false;
        }
        else
        {
            //set any door that isn't visible to visible
            GetComponent<MeshRenderer>().enabled = true;
            //play the door open animation
            myAnimator.Play("Door" + doorNumber + " Open");
            //set the door to not open
            openState = true;
        }
    }

    public void ChangeWall()
    {
        if (attemptClose)
        {
            //setting random value to closeChance
            closeChance = Random.Range(0f, 10f);
            //close the door
            if (closeChance < requiredChance)
            {
                //if door is already closed
                if(openState)
                {
                    //play door closing animation
                    myAnimator.Play("Door" + doorNumber + " Closing");
                    //play audio
                    GetComponent<AudioSource>().Play();
                }

                //TEMP set material to red material (has been blocked off)
                visualIdentifierObject.GetComponent<MeshRenderer>().material = visualMaterial[1];

                //set open state to false
                openState = false;
            }
            //open the door
            else
            {
                //if door is closed
                if (!openState)
                {
                    //play door opening animation
                    myAnimator.Play("Door" + doorNumber + " Opening");
                    //play audio
                    GetComponent<AudioSource>().Play();
                }

                //TEMP set material to green material (has NOT been blocked off)
                visualIdentifierObject.GetComponent<MeshRenderer>().material = visualMaterial[0];

                //set open state to true
                openState = true;
            }
            Invoke("ChangeWall", 5f);
        }
    }
}
