using UnityEngine;

public class audioManager : MonoBehaviour
{
    [Header("Audio Sources:")]
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource Soundeffect;
    [SerializeField] AudioSource Timedeffect;

    [Header("Audio Clips:")]
    public AudioClip Background;
    public AudioClip Buzz;
    public AudioClip whoop;

    private void Start()
    {
        music.clip = Background;
        music.Play();
        Soundeffect.clip = Buzz;
        Soundeffect.Play();
    }

    public void playsoundef(AudioClip clip)
    {
        Timedeffect.PlayOneShot(clip);
    }
}
