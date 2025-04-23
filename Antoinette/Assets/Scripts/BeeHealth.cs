using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeHealth : MonoBehaviour
{
 
    public int maxHealth = 2;
    [SerializeField] private int currentHealth = 2;
    public Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;
    private BeeUIManager _uiManager;

    audioManager audioManager;
    
private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _uiManager = GameObject.Find("GamePlayCanvasL2").GetComponent<BeeUIManager>();
        _uiManager.UpdateBeeLifes(currentHealth);
    }

    public void TakeDamage()
    {
        
        if (isDead) return;

        StartCoroutine(DamageAnimation());
        currentHealth--;
        _uiManager.UpdateBeeLifes(currentHealth);
        
        if (currentHealth == 0)
        { 
            Die();
        }
    }

    public void Die()
    {
        audioManager.stopBuzzing(audioManager.Soundef1);
        isDead = true;
        animator.SetTrigger("isDefeated");
        float b_force = 200f;
        rb.AddForce(transform.up * -b_force);

    }

    IEnumerator DamageAnimation()
    {
        animator.SetBool("isDamaged", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isDamaged", false);
        yield break;
    }

    public bool isDefeated()
    {
        return isDead;
    }
}
