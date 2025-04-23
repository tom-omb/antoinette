using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneLoopPoint : MonoBehaviour
{
    VideoPlayer vp;
    private SceneTransition transition;

    void Start()
    {
        vp = GetComponent<VideoPlayer>();
        vp.isLooping = false;
        vp.loopPointReached += OnLoopPointReached;
        vp.Play();

        transition = GameObject.Find("Scene Transition").GetComponent<SceneTransition>();
    }


    void OnLoopPointReached(VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10f;
        StartCoroutine(Next());
    }

    IEnumerator Next()
    {
        transition.NextLevel();
        yield return new WaitForSeconds(1f);
        vp.Play();
    }
}
