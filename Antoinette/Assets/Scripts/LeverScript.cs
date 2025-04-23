using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{

    public GameObject ant_obj;
    private Rigidbody2D ant_body;
    public GameObject lever_obj;

    Vector3 downPos;
    Vector3 upPos;

    private bool isTriggered = false;
    [Header("Audio Clips")]
    public AudioClip leverDownSound;
    public AudioClip tickingSound;
    public AudioClip dingSound;
    private AudioSource audioSource;



    void Start()
    {
        ant_body = ant_obj.GetComponent<Rigidbody2D>();
        upPos = lever_obj.transform.position;
        downPos = new Vector3(upPos.x, upPos.y - 0.2f, upPos.z);
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("Missing AudioSource on " + gameObject.name);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggered) { // this is to make sure it doesnt trigger during the sequence
            isTriggered = true;
            StartCoroutine(LeverDown());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isTriggered = false;
    }

    IEnumerator LeverDown()
    {
        yield return new WaitForSeconds(0.1f); // makes it look smoother idk

        // LEVER GOING DOWN
        float elapsedTime = 0f;
        float endTime = 1f;

        if (audioSource != null && leverDownSound != null)
        {
            audioSource.PlayOneShot(leverDownSound);
        }

        while (elapsedTime < endTime)
        {
            transform.position = Vector3.Lerp(upPos, downPos, elapsedTime / endTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = downPos;
        
        // TODO: add clock sound effect
        if (audioSource != null && tickingSound != null)
        {
            audioSource.clip = tickingSound;
            audioSource.loop = true;
            audioSource.Play();
        }

        yield return new WaitForSeconds(2f); 

        if (audioSource != null && audioSource.isPlaying && audioSource.clip == tickingSound)
        {
            audioSource.Stop();
            audioSource.loop = false;
            audioSource.clip = null; // clean reset
        }


        // LEVER GOING UP
        while (elapsedTime < endTime)
        {
            transform.position = Vector3.Lerp(downPos, upPos, elapsedTime / endTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // TODO: add "ding" sound effect
        transform.position = upPos;

        if (audioSource != null && dingSound != null)
        {
            audioSource.PlayOneShot(dingSound);
        }


        lever_obj.GetComponent<Collider2D>().isTrigger = true;
        // ^^ makes it so ant can pass through the lever temporarily to avoid collisions stopping ant from going up
        if (isTriggered)
        {
            ant_body.AddForce(transform.up * 400f); // lauches antoinette UP
        }

        yield return new WaitForSeconds(0.5f);
        lever_obj.GetComponent<Collider2D>().isTrigger = false;
        isTriggered = false;
        yield break;
    }
}
