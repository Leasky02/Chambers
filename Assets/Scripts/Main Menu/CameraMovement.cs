using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //variable controlling speed of rotation
    private float scalingFactor = 2f;

    [SerializeField] private Transform target;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        //below moves object in a full circle, keeping focus on one point in the centre

        transform.LookAt(target);
        transform.Translate(Vector3.right * Time.deltaTime * scalingFactor);
    }
}
