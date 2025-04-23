using UnityEngine;

public class audioManager : MonoBehaviour
{
    [Header("Audio Sources:")]
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource Soundeffect;
    [SerializeField] AudioSource Timedeffect;
    

    [Header("Audio Clips:")]
    public AudioClip Background;
    public AudioClip Soundef1;
    public AudioClip Soundef2;
    



    private void Start()
    {
        music.clip = Background;
        music.Play();
        Soundeffect.clip = Soundef1;
        Soundeffect.Play();
    }

    public void playsoundef(AudioClip clip)
    {
        Timedeffect.PlayOneShot(clip);
    }

    public void stopsound()
    {    
        music.Stop();    
        Soundeffect.Stop();
    }

    public void stopSoundEffect()
    {
        Soundeffect.Pause();
    }

    public void playsound()
    {
        music.Play();
        Soundeffect.Play();
    }

    public void stopBuzzing(AudioClip Clip)
    {
        Soundeffect.Stop();
    }
}
