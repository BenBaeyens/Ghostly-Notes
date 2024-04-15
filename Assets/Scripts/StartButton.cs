using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public string gameSceneName = "BenScene"; 
    public GameObject canvas;

    private Animator animator;

    private void Start() {
        if(canvas != null)
            animator = canvas.GetComponent<Animator>();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameWithAnimation());
    }

    IEnumerator StartGameWithAnimation()
    {
        if(animator != null)
        {        
            animator.SetTrigger("StartGame");
            yield return new WaitForSeconds(3.7f);
        }
        yield return null;
        SceneManager.LoadScene(gameSceneName);
    }
}
