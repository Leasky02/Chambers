using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScrollScript : MonoBehaviour
{
    //variable to determine if credits shoud move
    private bool rollCredits;
    //force applied to credits
    public float scalingFactor;

    //position where credits start
    private Vector3 startPosition;


    private void Start()
    {
        //get start position of the credits
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    void Update()
    {
        //if credits should roll, move them with force
        if (rollCredits)
            transform.Translate(Vector3.up * Time.deltaTime * scalingFactor);
    }
    //method to move credtis
    public void QueueCredits(bool move)
    {
        rollCredits = move;
        if(!move)
        {
            //sets credits back to the start
            transform.position = startPosition;
        }

    }
}
