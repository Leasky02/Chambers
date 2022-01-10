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
    public bool chase;
    public bool checkToStop = true;

    //speed
    private float speed;

    //original position
    private Vector3 originalPosition;

    //awake
    private void Start()
    {
        //set navmesh to component
        navMesh = GetComponent<NavMeshAgent>();
        //find player transform in scene
        player = GameObject.Find("Player");
        //set original position to current position
        originalPosition = transform.position;
        //set speed to original speed
        speed = navMesh.speed;
    }

    // Update is called once per frame
    void Update()
    {
        //if player is within 18 units of self and isnt already chasing the player and player is in the chambers
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < 18f && chase == false && player.transform.position.y <= 8.96f)
        {
            //chase player
            chase = true;
            enemyAnimator.GetComponent<buttonControl_script>().StartRun();
        }
        //if player is further than 32 units away from self and is currently chasing the player
        else if(Vector3.Distance(gameObject.transform.position, player.transform.position) > 25f && chase == true)
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
        if (Vector3.Distance(gameObject.transform.position, navMesh.destination) < 20 && checkToStop && !chase)
        {
            enemyAnimator.GetComponent<buttonControl_script>().EndCrippledWalk();
            enemyAnimator.GetComponent<buttonControl_script>().Idle();
            //chase after nothing
            navMesh.destination = transform.position;
            checkToStop = false;
            Invoke("RandomLocation", Random.Range(1f, 2f));
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

    public void RandomLocation()
    {
        navMesh.speed = speed / 2;
        //check to stop 
        checkToStop = true;
        //play walking animation
        enemyAnimator.GetComponent<buttonControl_script>().EndRun();
        enemyAnimator.GetComponent<buttonControl_script>().StartCrippledWalk();
        //set position randomly within chambers
        navMesh.destination = new Vector3(Random.Range(-95f, 125f), transform.position.y, Random.Range(-120f, 120f));
        navMesh.stoppingDistance = 20;
    }

    public void Celebrate()
    {
        //chase is false
        chase = false;
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
        navMesh.stoppingDistance = 1.3f;
        //speed enemy up
        navMesh.speed = speed * 2;
    }

    public void StopChase()
    {
        //stop chasing player
        chase = false;
        //slow enemy down
        navMesh.speed = speed / 2;
        //Move to random location
        RandomLocation();
    }
}
