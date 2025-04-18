using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class DeathOnTrigger : MonoBehaviour
{
    //was named PotBottomTrigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AntHealth playerHealth = other.GetComponent<AntHealth>();
 
            if (playerHealth != null)
            {
                playerHealth.SetHealthToZero();
            }
        }
    }
}
 
 