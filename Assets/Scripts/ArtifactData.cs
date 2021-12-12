using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactData : MonoBehaviour
{
    //variables containing artifact value
    public int value;

    //variable containing artifact class
    //1 = low, 2 = medium, 3 = high
    private int artifactClass;

    // Start is called before the first frame update
    void Start()
    {
        //set Y rotation randomly
        transform.rotation = Quaternion.Euler(Random.Range(0f,180f), Random.Range(0f, 360f), 0f);

        //set class randomly
        artifactClass = Random.Range(1,4);

        switch(artifactClass)
        {
            case 1:
                //set the value
                value = 50;
                //set value randomly
                value = Random.Range(value - 10, value + 10);
                break;
            case 2:
                //set the value
                value = 80;
                //set value randomly
                value = Random.Range(value - 10, value + 10);
                break;
            case 3:
                //set the value
                value = 120;
                //set value randomly
                value = Random.Range(value - 10, value + 10);
                break;
        }
    }
}
