using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverallScoreDisplay : MonoBehaviour
{
    [SerializeField] private Text overallScoreDisplay;
    // Start is called before the first frame update
    void Start()
    {
        overallScoreDisplay.text = "£" + PlayerPrefs.GetFloat("overallScore");
    }
}
