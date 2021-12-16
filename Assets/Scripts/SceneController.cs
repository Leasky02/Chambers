using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    //transitionObject
    [SerializeField] private GameObject sceneTransitions;

    private void Start()
    {
        //call function to set transition layer to 0
        Invoke("SetLayer", 1.2f);
    }

    public void ChangeScene(string sceneToLoad)
    {
        StartCoroutine(LoadScene(sceneToLoad));
    }
    //used to put transition UI elemnt to back
    public void SetLayer()
    {
        //set sorting order to 0
        sceneTransitions.GetComponent<Canvas>().sortingOrder = 0;
    }

    IEnumerator LoadScene(string sceneToLoad)
    {
        //set time to normal speed
        Time.timeScale = 1f;
        //start exit transition
        sceneTransitions.GetComponent<Animator>().Play("Exit");
        sceneTransitions.GetComponent<Canvas>().sortingOrder = 2000;
        //wait for 1 second
        yield return new WaitForSeconds(1);
        //load scene
        //set game and music started to false
        EventController.gameStarted = false;
        EventController.musicStarted = false;
        EventController.gameOver = false;
        PauseMenuController.allowPause = false;
        SceneManager.LoadScene(sceneToLoad);
    }
}
