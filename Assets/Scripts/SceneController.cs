using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    //transitionObject
    [SerializeField] private GameObject sceneTransitions;

    public void ChangeScene(string sceneToLoad)
    {
        StartCoroutine(LoadScene(sceneToLoad));
    }
    IEnumerator LoadScene(string sceneToLoad)
    {
        //start exit transition
        sceneTransitions.GetComponent<Animator>().Play("Exit");
        sceneTransitions.GetComponent<Canvas>().sortingOrder = 2000;
        //wait for 1 second
        yield return new WaitForSeconds(1);
        //load scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
