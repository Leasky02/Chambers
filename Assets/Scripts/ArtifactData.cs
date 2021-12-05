using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactData : MonoBehaviour
{
    //variables containing artifact value
    [HideInInspector]public int value;
    //variable controlling the object scale;
    private float scale;

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
                //set the scale
                scale = 1.3f;
                transform.localScale = new Vector3(scale, scale, scale);
                //set the value
                value = 50;
                break;
            case 2:
                //set the scale
                scale = 1.7f;
                transform.localScale = new Vector3(scale, scale, scale);
                //set the value
                value = 80;
                break;
            case 3:
                //set the scale
                scale = 2.2f;
                transform.localScale = new Vector3(scale, scale, scale);
                //set the value
                value = 120;
                break;
        }
        //Debug.Log(artifactClass);
    }
}
