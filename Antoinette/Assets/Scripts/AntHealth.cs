using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AntHealth : MonoBehaviour
{
   
    [SerializeField]
    private int health = 3;
    private UIManagerA uiManager;
    private Animator animator;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManagerA>();
        animator = GetComponent<Animator>();

        if (uiManager == null)
        {
            Debug.LogError("UIManager not found in the scene");
        }
        else
        {
            uiManager.UpdateLifes(health); // Update UI at start
        }
    }

    public void HealthGained()
    {
        if (health < 3) // Max health is 3
        {
            health++;
            uiManager.UpdateLifes(health);
        }

    }

    public void HealthLost()
    {
        StartCoroutine(DamageAnimation());
        health--;
        uiManager.UpdateLifes(health);
        if (health <= 0)
        {
            Die();
        }
    }

    public void SetHealthToZero()
    {
        StartCoroutine(DamageAnimation());
        health = 0;
        uiManager.UpdateLifes(health);
        Die();
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator DamageAnimation()
    {
        animator.SetBool("IsDamaged", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("IsDamaged", false);
        yield break;
    }
}