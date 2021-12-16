using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //player position
    private GameObject player;

    //bool to allow damage 
    private bool doDamage = true;
    //damage
    [HideInInspector] public float damage;

    //start called on the first frame
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        //if player is within 2 and can do damage and game isnt already over
        if(Vector3.Distance(gameObject.transform.position, player.transform.position) < 2f && doDamage && !EventController.gameOver)
        {
            //take damage
            player.GetComponent<PlayerTarget>().TakeDamage(damage);
            //stop enemy from doing damage for 2 seconds
            doDamage = false;
            Invoke("AllowDamage", 1.5f);
            //move enemy backwards with impulse force
        } 
    }

    public void AllowDamage()
    {
        doDamage = true;
    }
}
