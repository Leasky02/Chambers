using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactSpawnManager : MonoBehaviour
{
    //variable to say if it is a large or small room
    [SerializeField] private bool isSmallRoom;
    //variable containing how many spawn attempts to make
    [SerializeField] private int spawnAttempts;

    //variable to hold maximum x value of roomm
    [SerializeField] private int roomX1;
    [SerializeField] private int room2;
    //variable to hold maximum z value of roomm
    [SerializeField] private int roomZ1;
    [SerializeField] private int roomZ2;

    //variable holding all artifact prefabs
    [SerializeField] private GameObject[] ArtifactPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        //spawn artifacts as long as number of attempts is less than specified spawn attemps
        for(int i=0; i<= spawnAttempts; i++)
        {
            //Instantiate Artifact within Random Location
        }
    }

    public void SpawnItem()
    {

    }
}
