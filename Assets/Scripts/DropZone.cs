using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    //variable holding the game object containing the score
    [SerializeField] private GameObject score;
    //variable holding the cash sound
    [SerializeField] private AudioClip cashSound;

    //when object collides
    private void OnCollisionEnter(Collision collision)
    {
        //if object is an artifact
        if(collision.collider.CompareTag("Artifact"))
        {
            Debug.Log(collision.gameObject + "This should only be shown once");
            //add the score of the artifact to the score object
            score.GetComponent<Score>().AddScore(collision.gameObject.GetComponent<ArtifactData>().value);
            RemoveArtifact(collision.gameObject);
        }
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
