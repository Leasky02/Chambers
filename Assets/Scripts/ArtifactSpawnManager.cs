using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactSpawnManager : MonoBehaviour
{
    //variable containing how many spawn attempts to make
    [SerializeField] private int spawnAttempts;

    //variable to hold maximum x value of roomm
    [SerializeField] private float roomX1;
    [SerializeField] private float roomX2;
    //variable to hold maximum z value of roomm
    [SerializeField] private float roomZ1;
    [SerializeField] private float roomZ2;

    //variable holding all artifact prefabs
    [SerializeField] private GameObject[] ArtifactPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        //spawn artifacts as long as number of attempts is less than specified spawn attemps
        for(int i=0; i<= spawnAttempts; i++)                                                        
        {
            //create a variable set to a random value
            float spawnValue = Random.Range(0, 10);
            //if the spawn value is > spawn value required...
            if (spawnValue > 7)
            {
                //make a random position based on room boundries
                Vector3 randomPosition = new Vector3(Random.Range(roomX1, roomX2), 3f, Random.Range(roomZ1, roomZ2));

                Vector3 rotatedRandomPosition = new Vector3(randomPosition.x, randomPosition.y, randomPosition.z);

                //if room is rotated 90...
                if (transform.localRotation.eulerAngles.y == 90)
                {
                    //adjust x and y values accordingly
                    rotatedRandomPosition.x = randomPosition.z;
                    rotatedRandomPosition.z = -randomPosition.x;
                }
                //if room is rotated 180...
                else if (transform.localRotation.eulerAngles.y == 180)
                {
                    //adjust x and y values accordingly
                    rotatedRandomPosition.x = -randomPosition.x;
                    rotatedRandomPosition.z = -randomPosition.z;
                    //Destroy(gameObject);
                    //doDestroy = true;
                }
                //if room is rotated 270...
                else if (transform.localRotation.eulerAngles.y == 270)
                {
                    //adjust x and y values accordingly
                    rotatedRandomPosition.x = -randomPosition.z;
                    rotatedRandomPosition.z = randomPosition.x;
                }
                //add random rotated position to global object position
                rotatedRandomPosition.x += transform.position.x;
                rotatedRandomPosition.z += transform.position.z;

                //Instantiate Artifact within Random Locations
                //create random object
                GameObject tempArtifact = Instantiate(ArtifactPrefabs[Random.Range(0, 5)], rotatedRandomPosition, Quaternion.identity);
            }
        }
    }
}
