using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class particleStstemDamage : MonoBehaviour
{

    private float damageCooldown = 2f; // collision -> Damage -> two secounds -> Damage again
    private bool canTakeDamage = true;

    void OnParticleCollision(GameObject other)  //the collision message should be enabled
    {
        if (other.CompareTag("Player") && canTakeDamage) 
        {
            AntHealth playerHealth = other.GetComponent<AntHealth>();
 
            if (playerHealth != null)
            {
                playerHealth.HealthLost();
                StartCoroutine(DamageCooldown());
            }
        }
    }            
    IEnumerator DamageCooldown() 
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    } 
}
