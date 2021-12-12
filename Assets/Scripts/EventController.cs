using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private GameObject timerManager;
    [SerializeField] private Animator doorAnimator;

    //called on the first frame
    private void Start()
    {
        //queue the game to start in x seconds
        Invoke("StartTimer", 1f);
        Invoke("StartAnimation", 6.2f);
        Invoke("PlayMusic", 5.1f);
    }

    public void StartTimer()
    {
        //start timer
        timerManager.GetComponent<Timer>().runTimer = true;
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
}
