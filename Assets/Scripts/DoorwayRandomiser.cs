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

    //TEMP testing variable to summon an object to visualise what doors can potentially close
    [SerializeField] private GameObject visualIdentifierPrefab;
    //TEMP variable to contain the instantiated identifier
    private GameObject visualIdentifierObject;
    //TEMP variable to contain materials for object visualisation
    [SerializeField] private Material[] visualMaterial;

    private void Start()
    {
        if (attemptClose)
        {
            //TEMP Create position from object position
            Vector3 position = GetComponent<Transform>().position;
            //TEMP set y position up higher above the level
            position.y = position.y + 10;
            //TEMP create object with position above
            visualIdentifierObject = Instantiate(visualIdentifierPrefab, position, Quaternion.identity);

            //do the wall change function in 1 seconds
            Invoke("ChangeWall", 1f);
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
                    //play door open animation
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
