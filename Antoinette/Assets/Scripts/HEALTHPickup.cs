using UnityEngine;

public class HEALTHPickup : MonoBehaviour
{
    private AntHealth antHealth;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            antHealth = player.GetComponent<AntHealth>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (antHealth != null)
            {
                antHealth.HealthGained();
                Destroy(this.gameObject);
            }
        }
    }
}