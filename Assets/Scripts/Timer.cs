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
    private bool takingAway = false;
    private bool runTimer = false;

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
            StartCoroutine(TakeTime());
        }
    }

    public void RunTimer()
    {
        runTimer = true;
    }

    IEnumerator TakeTime()
    {
        //set variable to say it is currently in the process of changing the time
        takingAway = true;
        //wait for 1 second
        yield return new WaitForSeconds(1f);
        //remove 1 second
        seconds--;
        //if seconds is less than 0 AND there is more than 1 minute left, set it to 59
        if(seconds < 0 && minutes > 0)
        {
            seconds = 59;
            minutes--;
        }

        //if there is less than 10 seconds left
        if(seconds < 10)
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
        //set variable to say it has FINISHED changing the time
        takingAway = false;
    }

    public void EndGame()
    {
        //end the game
        eventManager.GetComponent<EventController>().EndGameCheck();
    }
}
