using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMusic : MonoBehaviour
{
    [Header("Audio Sources:")]
    [SerializeField] AudioSource music;
    

    [Header("Audio Clips:")]
    public AudioClip Music;
    void Start()
    {
        music.clip = Music;
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
