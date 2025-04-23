using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    [SerializeField] private Animator transitionAnim;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    public IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene((CutsceneManager.currentScene + 1) % 3);
        Time.timeScale = 1f;
        transitionAnim.SetTrigger("Start");
    }
}
