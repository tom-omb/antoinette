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
    private UIManager _uiManager;
    
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiManager.UpdateLifes(currentHealth-1);
    }

    public void TakeDamage()
    {
        
        if (isDead) return;
        
        currentHealth--;
        _uiManager.UpdateLifes(currentHealth-1);
        animator.SetTrigger("isDamaged");

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

        yield return new WaitForSeconds(1f);
        rb.AddForce(transform.up * -b_force);
        yield return new WaitForSeconds(2f);
        rb.velocity = Vector2.zero;
        this.enabled = false;
    }
}
