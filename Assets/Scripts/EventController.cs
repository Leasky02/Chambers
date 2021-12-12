using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    //variables containing UI element canvases
    [SerializeField] private GameObject artifactCanvas;
    [SerializeField] private GameObject timerCanvas;
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject rifle;

    //win screen canvas
    [SerializeField] private GameObject winScreenCanvas;
    //lose screen canvas
    [SerializeField] private GameObject loseScreenCanvas;

    //music
    [SerializeField] private AudioSource musicPlayer;
    //timer
    [SerializeField] private GameObject timerManager;
    //door entrance
    [SerializeField] private Animator doorAnimator;
    //score manager
    [SerializeField] private Text scoreDisplay;
    [SerializeField] private GameObject scoreManager;

    //player
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainCamera;

    //variable to say if game has started
    private bool gameStarted = false;

    //contains if player is in or out of the chambers
    private bool isOutside;

    private void Update()
    {
        //if player is out side chambers above y=10
        if(player.transform.position.y >= 10)
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
    }

    public void StartGame()
    {
        //queue the game to start in x seconds
        StartTimer();
        Invoke("StartAnimation", 7f);
        Invoke("PlayMusic", 4.5f);
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

        //disable other UI components
        artifactCanvas.SetActive(false);
        timerCanvas.SetActive(false);

        //disable player movement
        player.GetComponent<PlayerMovement>().enabled = false;
        mainCamera.GetComponent<MoveLook>().enabled = false;

        //disable the guns
        pistol.SetActive(false);
        rifle.SetActive(false);

        //set mouse to visible
        //locks cursor to game
        Cursor.lockState = CursorLockMode.None;
    }

    public void WinGame()
    {
        //set the win screen to visible
        winScreenCanvas.SetActive(true);
        //set score display
        scoreDisplay.text = ("£" + scoreManager.GetComponent<Score>().score);
    }

    public void LoseGame()
    {
        //set the lose screen to visible
        loseScreenCanvas.SetActive(true);
    }
}
