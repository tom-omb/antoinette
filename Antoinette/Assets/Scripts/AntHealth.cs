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

    private void Start()
    {
        AntHealthUIManager = FindObjectOfType<AntUIManager>();
        animator = GetComponent<Animator>();

        if (AntHealthUIManager == null)
        {
            Debug.LogError("Ant Health UIManager not found in the scene");
        }
        else
        {
            AntHealthUIManager.UpdateAntLifes(currentHealth); // Update UI at start
        }
    }

    public void HealthGained()
    {
        if(currentHealth != maxHealth){ // Max health is 3
            currentHealth++;
            AntHealthUIManager.UpdateAntLifes(currentHealth);
        }
        
    }

    public void HealthLost()
    {
        StartCoroutine(DamageAnimation());
        currentHealth--;
        AntHealthUIManager.UpdateAntLifes(currentHealth);
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
        DeathUIManager.SetActive(true); 
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