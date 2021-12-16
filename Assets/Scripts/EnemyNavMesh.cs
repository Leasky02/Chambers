using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    //variable for nav mesh component
    private NavMeshAgent navMesh;
    //player position
    private GameObject player;
    //variable for the animator
    [SerializeField] private GameObject enemyAnimator;

    //variable to determine if should chase player
    private bool chase;
    private bool checkToStop;

    //original position
    private Vector3 originalPosition;

    //awake
    private void Awake()
    {
        //set navmesh to component
        navMesh = GetComponent<NavMeshAgent>();
        //find player transform in scene
        player = GameObject.Find("Player");
        //set original position to current position
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if player is within 18 units of self and isnt already chasing the player
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < 18f && chase == false)
        {
            //chase player
            chase = true;
            //speed enemy up
            navMesh.speed = navMesh.speed * 2;
            enemyAnimator.GetComponent<buttonControl_script>().StartRun();
        }
        //if player is further than 32 units away from self and is currently chasing the player
        else if(Vector3.Distance(gameObject.transform.position, player.transform.position) > 32f && chase == true)
        {
            //end chase
            StopChase();
        }
        //if should chase player
        if(chase)
        {
            //chase player
            ChasePlayer();
        }
        //if enemy is home
        if(Vector3.Distance(gameObject.transform.position, new Vector3(originalPosition.x, gameObject.transform.position.y, originalPosition.z)) < 3 && checkToStop)
        {
            enemyAnimator.GetComponent<buttonControl_script>().EndCrippledWalk();
            enemyAnimator.GetComponent<buttonControl_script>().Idle();
            //chase after nothing
            navMesh.destination = transform.position;
            checkToStop = false;
        }
        if(EventController.gameOver)
        {
            //if player has lost game
            if (player.transform.position.y < 10.4f)
            {
                //celebrate
                Invoke("Celebrate", Random.Range(0f, 2f));
            }
            else
            {
                //stop and dance
                enemyAnimator.GetComponent<buttonControl_script>().EndRun();
                enemyAnimator.GetComponent<buttonControl_script>().EndCrippledWalk();
                navMesh.destination = transform.position;
            }
        }
    }

    public void Celebrate()
    {
        //stop and dance
        enemyAnimator.GetComponent<buttonControl_script>().EndRun();
        enemyAnimator.GetComponent<buttonControl_script>().EndCrippledWalk();
        enemyAnimator.GetComponent<buttonControl_script>().Dance();
        navMesh.destination = transform.position;
    }

    public void ChasePlayer()
    {
        //chase after players position
        navMesh.destination = player.transform.position;
    }

    public void StopChase()
    {
        //stop chasing player
        chase = false;
        //slow enemy down
        navMesh.speed = navMesh.speed / 2;
        //return to original position
        navMesh.destination = originalPosition;
        //play walking animation
        enemyAnimator.GetComponent<buttonControl_script>().EndRun();
        enemyAnimator.GetComponent<buttonControl_script>().StartCrippledWalk();
        checkToStop = true;
    }
}
