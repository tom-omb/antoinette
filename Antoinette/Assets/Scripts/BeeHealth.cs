using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeHealth : MonoBehaviour
{

    public int maxHealth = 3;
    [SerializeField]
    private int currentHealth = 3;
    public Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;
   
    private float b_force = 150f;
    private UIManagerB _uiManager;
    
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManagerB>();
        _uiManager.UpdateLifes1(currentHealth-1);
    }

    public void TakeDamage()
    {
        
        if (isDead) return;

        StartCoroutine(DamageAnimation());
        currentHealth--;
        _uiManager.UpdateLifes1(currentHealth-1);
        
        if (currentHealth == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("isDefeated");

        // GetComponent<BeetriceWingAttack>().enabled = false;
        StartCoroutine(FallToGround());

    }
    IEnumerator FallToGround()
    {
        rb.AddForce(transform.up * -b_force);
        rb.velocity = Vector2.zero;
        yield break;
    }

    IEnumerator DamageAnimation()
    {
        animator.SetBool("isDamaged", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isDamaged", false);
        yield break;
    }
}
