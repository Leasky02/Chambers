using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    //variable contiaing sound source
    [SerializeField] private AudioSource soundPlayer;
    // Start is called before the first frame update
    
    public void PlaySound()
    {
        //play sound source
        soundPlayer.Play();
    }
}
