using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    private AudioSource myAudioSource;

    [SerializeField] private AudioClip[] groan;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        InvokeRepeating("GroanSound", 1f, Random.Range(4f, 10f));
    }

    public void GroanSound()
    {
        myAudioSource.clip = groan[Random.Range(0, 6)];
        myAudioSource.Play();
    }
}


