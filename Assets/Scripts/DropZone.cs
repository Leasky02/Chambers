using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    //variable holding the game object containing the score
    [SerializeField] private GameObject score;
    //variable holding the cash sound
    [SerializeField] private AudioClip cashSound;
    //variable to check for collisions
    private bool checkCollisions = true;

    //when object collides
    private void OnCollisionEnter(Collision collision)
    {
        //if object is an artifact
        if(collision.collider.CompareTag("Artifact") && checkCollisions)
        {
            //stop from detecting collisions with artifacts
            checkCollisions = false;
            Invoke("ChangeCollisionDetection", 0.1f);

            //add the score of the artifact to the score object
            score.GetComponent<Score>().AddScore(collision.gameObject.GetComponent<ArtifactData>().value);
            RemoveArtifact(collision.gameObject);
        }
    }
    //used to restart detecting for artifacts
    public void ChangeCollisionDetection()
    {
        checkCollisions = true;
    }
    //method to destroy artifact and trigger particle effect
    public void RemoveArtifact(GameObject artifact)
    {
        //disable components on artifact
        artifact.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        artifact.GetComponent<Rigidbody>().isKinematic = true;
        artifact.GetComponent<InventorySlot>().enabled = false;

        //play particle system
        artifact.GetComponent<ParticleSystem>().Play();

        //set audio clip of audio source to cash sound
        artifact.GetComponent<AudioSource>().clip = cashSound;
        artifact.GetComponent<AudioSource>().Play();

        //destroy artifact
        Destroy(artifact, 3f);
    }
}
