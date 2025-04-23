using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public static int currentScene = 0;
    [SerializeField]
    private GameObject[] Scenes = new GameObject[3];


    void Update()
    {
        if(currentScene > 2)
        {
            currentScene = 2;
        }

        Scenes[currentScene].SetActive(true);
    }
}
