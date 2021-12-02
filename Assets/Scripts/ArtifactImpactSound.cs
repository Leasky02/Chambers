using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactImpactSound : MonoBehaviour
{
    //variable holding all impact sounds
    [SerializeField] private AudioClip[] potImpact;

    //variable holding the audio source
    private AudioSource audioSrc;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            //set audio clip randomly
            audioSrc.clip = potImpact[Random.Range(0, 3)];
            //set the pitch randomly
            audioSrc.pitch = Random.Range(0.9f, 1.1f);
            //play the audio
            audioSrc.Play();
        }
    }
}
