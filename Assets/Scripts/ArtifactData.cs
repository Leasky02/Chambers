using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactData : MonoBehaviour
{
    //variables containing artifact value
    public int value;
    //variables containing upper and lower limit for random value
    [SerializeField] private int[] valueLimits = new int[2];
    //variable containing artifact class
    //0 = low, 1 = medium, 2 = high
    public int artifactClass;

    // Start is called before the first frame update
    void Start()
    {
        //set value randomly between 2 values
        value = Random.Range(valueLimits[0], valueLimits[1]);

        //set Y rotation randomly
        transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
    }
}
