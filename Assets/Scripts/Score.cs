using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //variable containing the score
    [HideInInspector] public int score;
    //variable to display the text
    [SerializeField] private Text scoreDisplay;
    //Start called once when created
    private void Start()
    {
        //display the value of score
        scoreDisplay.text = ("Value: £" + score);
    }
    //method to add score
    public void AddScore(int scoreAdded)
    {
        //add value to current score
        score += scoreAdded;
        //display the new value
        scoreDisplay.text = ("Value: £" + score);
    }
}
