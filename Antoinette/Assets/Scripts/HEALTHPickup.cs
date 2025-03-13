using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HEALTHPickup : MonoBehaviour
{
    [SerializeField]
    private AntHealth antHealth;


    void Start()
    {
        antHealth = GameObject.FindWithTag("Player").GetComponent<AntHealth>();
        if (antHealth == null)
        {
            Debug.LogError("AntHealth Component not found");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            antHealth.HealthGained();
            Destroy(this.gameObject);
        }
    }
}
