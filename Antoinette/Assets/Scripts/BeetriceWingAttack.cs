using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetriceWingAttack : MonoBehaviour
{
    public float sweepDistance = 3f;
    private bool isAttacking = false;
    public Animator animator;



    void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !GetComponent<BeeHealth>().isDefeated())
        {
            AntHealth playerHealth = collision.GetComponent<AntHealth>();
            playerHealth.HealthLost();
        }
    }

    public void StartWingAttack()
    {
        if (!isAttacking)
        {
            StartCoroutine(WingAttackSequence());
        }
    }

    IEnumerator WingAttackSequence()
    {
        
        isAttacking = true;
        animator.SetBool("isAttacking", isAttacking);

        float elapsedTime;

        float b_force = 150f;
        Rigidbody2D b_RB = GetComponent<Rigidbody2D>();
        b_RB.AddForce(transform.up * -b_force);

        yield return new WaitForSeconds(0.5f);
        b_RB.velocity = Vector3.zero;

        Vector3 sweepEndPos = transform.position + new Vector3(-sweepDistance, 0, 0);

        elapsedTime = 0f;
        float sweepTime = 1f;
        while (elapsedTime < sweepTime)
        {
            transform.position = Vector3.Lerp(transform.position, sweepEndPos, elapsedTime / sweepTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        
        yield break;
    }
}
