using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EventController : MonoBehaviour
{
    //variables containing UI element canvases
    [SerializeField] private GameObject artifactCanvas;
    [SerializeField] private GameObject timerCanvas;
    [SerializeField] private GameObject healthCanvas;
    [SerializeField] private GameObject scoreCanvas;
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject rifle;

    //win screen canvas
    [SerializeField] private GameObject winScreenCanvas;
    //lose screen canvas
    [SerializeField] private GameObject loseScreenCanvas;
    [SerializeField] private Text loseScreenMessage;

    //music
    [SerializeField] private AudioSource musicPlayer;
    //timer
    [SerializeField] private GameObject timerManager;
    //door entrance
    [SerializeField] private Animator doorAnimator;
    //drop zone object
    [SerializeField] private GameObject dropZone;

    //score manager
    [SerializeField] private Text scoreDisplay;
    [SerializeField] private Text overallScoreDisplay;
    [SerializeField] private GameObject scoreManager;

    //player
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainCamera;

    //animator for scores
    [SerializeField] private Animator scoreAnimations;

    //variable to say if game has started
    public static bool gameStarted = false;
    public static bool musicStarted = false;
    public static bool gameOver = false;

    //contains if player is in or out of the chambers
    private bool isOutside;

    //variable to tell script to add current score to overall score
    private bool switchScore;

    //temporary variables containing score
    private float tempScore;
    private float tempOverallScore;

    //variable to hold money sound
    [SerializeField] private AudioClip moneyComplete;
    //variable to hold lose sound
    [SerializeField] private AudioClip loseSound;

    private void Update()
    {
        //if player is out side chambers above y=10
        if(player.transform.position.y >= 8.96f)
        {
            //set is player outside to true
            isOutside = true;
        }
        else
        {
            //set is player outside to false
            isOutside = false;
            //if player is inside chamber and game hasnt already started
            if(!gameStarted)
            {
                //start game
                StartGame();
                gameStarted = true;
            }
        }
        Debug.Log(isOutside);
        //take one off score and add one to overall score
        if(switchScore)
        {
            if(tempScore > 0)
            {
                if(tempScore>5)
                {
                    //take two away from score for faster countup
                    tempScore -= 2;
                    //add two to overall score
                    tempOverallScore+=2;
                }
                else
                {
                    //add what is left of score 
                    tempOverallScore+=tempScore;
                    //set score to 0
                    tempScore = 0;
                }
                //display score
                scoreDisplay.text = ("£" + tempScore);
                //display overall score
                overallScoreDisplay.text = ("£" + tempOverallScore);
            }
            else
            {
                //start animator
                scoreAnimations.Play("Switch");
                //stop sound
                //play sound of score adding up
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = moneyComplete;
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource>().Play();
                switchScore = false;
            }
        }
    }

    public void StartGame()
    {
        //queue the game to start in x seconds
        StartTimer();
        Invoke("StartAnimation", 3f);
        Invoke("PlayMusic", 0.5f);
    }

    public void StartTimer()
    {
        //start timer
        timerManager.GetComponent<Timer>().RunTimer();
    }

    public void PlayMusic()
    {
        //play music
        musicPlayer.Play();
        musicStarted = true;
    }

    public void StartAnimation()
    {
        //start animation
        doorAnimator.Play("Closing");
    }

    public void EndGameCheck()
    {
        //if player is outside the chambers
        if(isOutside)
        {
            //play the win screen
            WinGame();
        }
        else
        {
            //play the lose screen
            LoseGame();
        }

        //disable drop zone
        dropZone.SetActive(false);
        //disable other UI components
        artifactCanvas.SetActive(false);
        timerCanvas.SetActive(false);
        scoreCanvas.SetActive(false);
        healthCanvas.SetActive(false);

        //disable player movement
        player.GetComponent<PlayerMovement>().enabled = false;
        mainCamera.GetComponent<MoveLook>().enabled = false;

        //disable the guns
        pistol.SetActive(false);
        rifle.SetActive(false);

        //set mouse to visible
        //locks cursor to game
        Cursor.lockState = CursorLockMode.None;

        //set game as over
        gameOver = true;
    }

    public void WinGame()
    {
        //set the win screen to visible
        winScreenCanvas.SetActive(true);
        //set score display
        scoreDisplay.text = ("£" + scoreManager.GetComponent<Score>().score);
        //set overallScore display
        overallScoreDisplay.text = ("£" + scoreManager.GetComponent<Score>().overallScore);

        //adds score to long term score
        scoreManager.GetComponent<Score>().SaveScore();
        //triggers score adding onto the main score counting down
        Invoke("StartTally", 0.7f);
    }

    public void LoseGame()
    {
        //set the lose screen to visible
        loseScreenCanvas.SetActive(true);
        loseScreenMessage.text = "YOU'RE TRAPPED!";

        //stop sound
        //play sound of score adding up
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = loseSound;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Play();
    }

    public void Die()
    {
        //hide health bar
        healthCanvas.SetActive(false);
        //stop music
        musicPlayer.Stop();
        //set the lose screen to visible
        loseScreenCanvas.SetActive(true);
        loseScreenMessage.text = "YOU DIED!";

        //stop sound
        //play sound of score adding up
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = loseSound;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Play();

        //disable other UI components
        artifactCanvas.SetActive(false);
        timerCanvas.SetActive(false);
        scoreCanvas.SetActive(false);

        //disable player movement
        player.GetComponent<PlayerMovement>().enabled = false;
        mainCamera.GetComponent<MoveLook>().enabled = false;

        //disable the guns
        pistol.SetActive(false);
        rifle.SetActive(false);

        //set mouse to visible
        //locks cursor to game
        Cursor.lockState = CursorLockMode.None;

        //set game as over
        gameOver = true;
    }

    private void StartTally()
    {
        //set tempOverallScore to overallScore
        tempScore = scoreManager.GetComponent<Score>().score;
        //set tempScore to score
        tempOverallScore = scoreManager.GetComponent<Score>().overallScore;

        //allows script to tally up score by switching score to overall score
        switchScore = true;
        //play sound of score adding up
        GetComponent<AudioSource>().Play();
    }
}
