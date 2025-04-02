using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnt : MonoBehaviour
{
    // Basically use this script for anything that damages antoinette by 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AntHealth playerHealth = other.GetComponent<AntHealth>();

            if (playerHealth != null)
            {
                playerHealth.HealthLost();
            }
        }
    }
}
