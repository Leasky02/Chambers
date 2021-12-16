
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    //health
    public float health;
    //death sound
    [SerializeField] private AudioClip deathSound;
    //remove damage from object
    public void TakeDamage(float amount)
    {
        //take away health
        health -= amount;
        //if health is less than 0, die
        if(health <= 0f)
        {
            Die();
        }
    }
    //death
    void Die()
    {
        //play particle system
        GetComponent<ParticleSystem>().Play();
        //set audio clip of audio source
        GetComponent<AudioSource>().clip = deathSound;
        //play audio
        GetComponent<AudioSource>().Play();
        //disable components
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<EnemyNavMesh>().enabled = false;
        //destroy model
        Destroy(gameObject.transform.GetChild(0).gameObject);
        //destroy parent object
        Destroy(gameObject,2f);
    }
}
