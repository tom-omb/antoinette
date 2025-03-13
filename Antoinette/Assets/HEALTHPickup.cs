using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
[SerializeField] 

    private AntHealth antHealth;


    void Start()
    {
        antHealth = GameObject.FindWithTag("Ant").GetComponent<AntHealth>();
        if(antHealth==null )
        {
            Debug.LogError("AntHealth Component not found");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ant"))
        {
            antHealth.HealthGained();
            Destroy(gameObject);
        }
    }
}
