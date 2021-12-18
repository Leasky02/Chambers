using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    //variables containing UI element canvases
    [SerializeField] private GameObject artifactCanvas;
    [SerializeField] private GameObject timerCanvas;
    [SerializeField] private GameObject MenuButtons;
    [SerializeField] private GameObject player;
    //guns
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject rifle;
    //music
    [SerializeField] private AudioSource music;
    //timer
    [SerializeField] private GameObject timer;

    //variable containing if game has been paused
    [HideInInspector] public bool paused = false;

    public static bool allowPause = false;

    //start called on first frame
    private void Start()
    {
        Invoke("AllowPause", 2f);
    }
    // Update is called once per frame
    void Update()
    {
        //if escape is pressed & game isnt already over & isnt already paused & is allowed to pause
        if (Input.GetKeyDown(KeyCode.Escape) && !EventController.gameOver && !paused && allowPause)
        {
            //PauseGame();
        }
    }

    public void PauseGame()
    {
        //disable audio source of player
        player.GetComponent<AudioSource>().enabled = false;
        //hide UI elements
        artifactCanvas.SetActive(false);
        timerCanvas.SetActive(false);
        //show menuButtons
        MenuButtons.SetActive(true);

        //set time scale to 0
        Time.timeScale = 0f;

        //disable guns
        pistol.GetComponent<Gun>().enabled = false;
        rifle.GetComponent<Gun>().enabled = false;

        //unlocks cursor from game
        Cursor.lockState = CursorLockMode.None;

        //pause music
        music.Pause();

        //set game as paused
        paused = true;

    }

    public void UnpauseGame()
    {
        //disable audio source of player
        player.GetComponent<AudioSource>().enabled = true;
        //show UI elements
        artifactCanvas.SetActive(true);
        timerCanvas.SetActive(true);
        //hide menuButtons
        MenuButtons.SetActive(false);

        //set time scale to 1
        Time.timeScale = 1f;

        //enable guns
        pistol.GetComponent<Gun>().enabled = true;
        rifle.GetComponent<Gun>().enabled = true;

        //locks cursor to game
        Cursor.lockState = CursorLockMode.Locked;
        //if music has started
        if(EventController.musicStarted)
        {
            //play music
            music.Play();
        }

        //set game as unpaused
        paused = false;
    }
    //allow game to be paused
    public void AllowPause()
    {
        allowPause = true;
    }
}
