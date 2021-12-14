using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //variables holding what minutes and seconds to start on
    [SerializeField] private int minutes;
    [SerializeField] private int seconds;

    //variable that contains timer text;
    [SerializeField] private Text displayText;
    [HideInInspector] public bool takingAway = false;
    //if timer should run
    private bool runTimer = false;
    //changeColour
    private bool changeColour = true;

    //event controller
    [SerializeField] private GameObject eventManager;

    //start called on the first frame
    private void Start()
    {
        //display text
        displayText.text = minutes + ":00";
    }
    // Update is called once per frame
    void Update()
    {
        if(!takingAway && runTimer)
        {
            //set variable to say it is currently in the process of changing the time
            takingAway = true;
            //wait for 1 second
            Invoke("TakeTime",1f);
        }
    }

    public void RunTimer()
    {
        runTimer = true;
    }

    public void TakeTime()
    {
        //remove 1 second
        seconds--;
        //if seconds is less than 0 AND there is more than 1 minute left, set it to 59
        if(seconds < 0 && minutes > 0)
        {
            seconds = 59;
            minutes--;
        }

        //if there is less than 10 seconds left
        if (seconds < 10)
        {
            displayText.text = minutes + ":0" + seconds;
        }
        else
        {
            displayText.text = minutes + ":" + seconds;
        }
        //if minutes and seconds are at 0 (finished)
        if(minutes == 0 && seconds == 0)
        {
            runTimer = false;
            //delay to test for win/lose
            Invoke("EndGame", 0.5f);
        }

        //if there is less than 30 seconds left
        if (seconds <= 30 && minutes == 0)
        {
            //if it should change to red
            if(changeColour)
            {
                //change to red
                displayText.color = new Color(1, 0, 0, 1);
            }
            //if not, change to white
            else
            {
                //change to white
                displayText.color = new Color(1, 1, 1, 1);
            }
            //set changeColour to opposite of itself
            changeColour = !changeColour;

        }

        //set variable to say it has FINISHED changing the time
        takingAway = false;
    }

    public void EndGame()
    {
        //end the game
        eventManager.GetComponent<EventController>().EndGameCheck();
    }
}
