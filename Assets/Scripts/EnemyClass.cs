using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyClass : MonoBehaviour
{
    //variable holding class
    [SerializeField] private int enemyClass;
    //variable containing the enemy health

    //static difficulty speed
    public static float difficultySpeed = 1f;
    public static float difficultyDamage = 1f;
    // Start is called before the first frame update
    void Awake()
    {
        //set class randomly between 1 and 3
        enemyClass = Random.Range(1, 4);

        //rotate randomly
        transform.rotation = Quaternion.Euler(Random.Range(0f, 180f), Random.Range(0f, 360f), 0f);

        switch (enemyClass)
        {
            //if class 1 (small fast)
            case 1:
                //set scale of child so it doesnt effect speed
                gameObject.transform.GetChild(0).localScale = new Vector3(1.8f, 1.8f, 1.8f);
                //set speed
                gameObject.GetComponent<NavMeshAgent>().speed = 3.2f * difficultySpeed;
                //set animator speed
                gameObject.transform.GetChild(0).GetComponent<Animator>().speed = gameObject.transform.GetChild(0).GetComponent<Animator>().speed * 1.9f;
                //set health
                gameObject.GetComponent<Target>().health = 21;
                //set damage
                gameObject.GetComponent<EnemyDamage>().damage = 7f * difficultyDamage;

                break;
            //if class 2 (medium size and speed)
            case 2:
                //set scale of child so it doesnt effect speed
                gameObject.transform.GetChild(0).localScale = new Vector3(2.75f, 2.75f, 2.75f);
                //set speed
                gameObject.GetComponent<NavMeshAgent>().speed = 2.3f * difficultySpeed;
                //set animator speed
                gameObject.transform.GetChild(0).GetComponent<Animator>().speed = gameObject.transform.GetChild(0).GetComponent<Animator>().speed * 1.6f;
                //set health
                gameObject.GetComponent<Target>().health = 28;
                //set damage
                gameObject.GetComponent<EnemyDamage>().damage = 12f * difficultyDamage;

                break;
            //if class 3 (large and slow)
            case 3:
                //set scale of child so it doesnt effect speed
                gameObject.transform.GetChild(0).localScale = new Vector3(3f, 3f, 3f);
                //set speed
                gameObject.GetComponent<NavMeshAgent>().speed = 1.7f * difficultySpeed;
                //set health
                gameObject.GetComponent<Target>().health = 40;
                //set damage
                gameObject.GetComponent<EnemyDamage>().damage = 18f * difficultyDamage;

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
