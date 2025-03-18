using UnityEngine;
using UnityEngine.SceneManagement;

public class AntHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 3;
    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();

        if (uiManager == null)
        {
            Debug.LogError("⚠️ UIManager not found in the scene!");
        }
        else
        {
            uiManager.UpdateHealthUI(health); // Update UI at start
        }
    }

    public void HealthGained()
    {
        if (health < 3) // Max health is 3
        {
            health++;
            uiManager.UpdateHealthUI(health);
        }
    }

    public void HealthLost()
    {
        health--;
        uiManager.UpdateHealthUI(health);
        if (health <= 0)
        {
            Die();
        }
    }

    public void SetHealthToZero()
    {
        health = 0;
        uiManager.UpdateHealthUI(health);
        Die();
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}