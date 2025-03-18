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

            if (antHealth == null)
            {
                Debug.LogError("⚠️ AntHealth component NOT found on Player!");
            }
        }
        else
        {
            Debug.LogError("⚠️ Player with tag 'Player' not found! Check if Antionette has the correct tag.");
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
            else
            {
                Debug.LogError("⚠️ AntHealth reference is NULL in HEALTHPickup!");
            }
        }
    }
}