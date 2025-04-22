using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchenlvlmusic : MonoBehaviour
{
    [Header("Audio Sources:")]
    [SerializeField] AudioSource music;


    [Header("Audio Clips:")]
    public AudioClip Background;
    void Start()
    {
        music.clip = Background;
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
