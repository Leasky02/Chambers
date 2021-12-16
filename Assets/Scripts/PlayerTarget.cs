using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTarget : MonoBehaviour
{
    //health
    public float health = 100;
    //difficulty multiplier for health
    public static float difficultyHealth = 1f;
    //health bar
    [SerializeField] private Slider healthBar;
    //event manager
    [SerializeField] private GameObject eventManager;
    //animator
    [SerializeField] private Animator damageAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //set health bar values
        healthBar.maxValue = health;
        healthBar.value = health;
        //regenerate health
        InvokeRepeating("Regenerate", 0f, 5f);
    }

    public void TakeDamage(float damageValue)
    {
        //take away damage
        health -= damageValue;
        //update health bar
        healthBar.value = health;
        //play damage sound
        healthBar.GetComponent<AudioSource>().Play();
        //play animator
        damageAnimator.Play("Damage");

        //add force backwards

        //if has no health, die
        if(health < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //play damage sound
        healthBar.GetComponent<AudioSource>().Play();
        eventManager.GetComponent<EventController>().Die();
    }
    
    public void Regenerate()
    {
        health += 5;
        if (health > 100)
            health = 100;
        //update health bar
        healthBar.value = health;
    }
}
