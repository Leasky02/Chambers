using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactData : MonoBehaviour
{
    //variables containing artifact value
    public int value;

    public static float difficultyValue;

    //variable containing artifact class
    //1 = low, 2 = medium, 3 = high
    public int artifactClass;

    //colours
    private Color blue = new Color(123f / 255f, 157f / 255f, 230f / 255f, 1f);
    private Color orange = new Color(230f / 255f, 107f / 255f, 21f / 255f, 1f);
    private Color purple = new Color(230f / 255f, 64f / 255f, 227f / 255f, 1f);

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
                //multiply value according to difficulty
                value = Mathf.RoundToInt(value * difficultyValue);
                //set colour of outline
                gameObject.transform.GetChild(0).GetComponent<Outline>().OutlineColor = blue;
                break;
            case 2:
                //set the value
                value = 80;
                //set value randomly
                value = Random.Range(value - 10, value + 10);
                //multiply value according to difficulty
                value = Mathf.RoundToInt(value * difficultyValue);
                //set colour of outline
                gameObject.transform.GetChild(0).GetComponent<Outline>().OutlineColor = orange;
                break;
            case 3:
                //set the value
                value = 120;
                //set value randomly
                value = Random.Range(value - 10, value + 10);
                //multiply value according to difficulty
                value = Mathf.RoundToInt(value * difficultyValue);
                //set colour of outline
                gameObject.transform.GetChild(0).GetComponent<Outline>().OutlineColor = purple;
                break;
        }
    }
}
