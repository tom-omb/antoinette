using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AntHealth : MonoBehaviour
{ 
    public int currentHealth = 1;
    [SerializeField] private int maxHealth = 3;
    private AntUIManager AntHealthUIManager;
    [SerializeField] private GameObject DeathUIManager;
    [SerializeField] private GameObject FailUIManager;
    private Animator animator;
        [Header("Audio Clips")]
    public AudioClip damageSound;
    public AudioClip deathSound;
    public AudioClip healthGainSound;

    private AudioSource audioSource;

    audioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
    }


    private void Start()
    {
        AntHealthUIManager = FindObjectOfType<AntUIManager>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (AntHealthUIManager == null)
        {
            Debug.LogError("Ant Health UIManager not found in the scene");
        }
        else
        {
            AntHealthUIManager.UpdateAntLifes(currentHealth); // Update UI at start
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on this GameObject.");
        }
    }

    public void HealthGained()
    {
        if(currentHealth != maxHealth){ // Max health is 3
            currentHealth++;
            AntHealthUIManager.UpdateAntLifes(currentHealth);
        }

        if (audioSource != null && healthGainSound != null)
            {
                audioSource.PlayOneShot(healthGainSound);
            }

    }

    public void HealthLost()
    {
        StartCoroutine(DamageAnimation());
        currentHealth--;
        AntHealthUIManager.UpdateAntLifes(currentHealth);
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
        if (currentHealth == 0)
        {
            SetHealthToZero();
        }
    }

    public void SetHealthToZero()
    {
        StartCoroutine(DamageAnimation());
        currentHealth = 0;
        AntHealthUIManager.UpdateAntLifes(currentHealth);
        Die();
    }

    private void Die()
    {
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
        DeathUIManager.SetActive(true);
        audioManager.stopsound();
        DeathUIManager.GetComponent<DeathMenuUI>().PauseGame();
    }

    public void LevelTwoFailed()
    {
        StartCoroutine(DamageAnimation());
        currentHealth = 0;
        AntHealthUIManager.UpdateAntLifes(currentHealth);
        Fail();
    }
    private void Fail()
    {
        FailUIManager.SetActive(true); 
        FailUIManager.GetComponent<FailMenuUI>().PauseGame();
    }

    IEnumerator DamageAnimation()
    {
        animator.SetBool("IsDamaged", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("IsDamaged", false);
        yield break;
    }
}