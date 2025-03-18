using UnityEngine;
using UnityEngine.SceneManagement;

public class AntHealth : MonoBehaviour
{
   
    [SerializeField]
    private int health = 3;
    private UIManagerA uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManagerA>();

        if (uiManager == null)
        {
            Debug.LogError("⚠️ UIManager not found in the scene!");
        }
        else
        {
            uiManager.UpdateLifes(health); // Update UI at start
        }
    }

    public void HealthGained()
    {
        if (health < 3) // Max health is 3
        {
            health++;
            uiManager.UpdateLifes(health);
        }

    }

    public void HealthLost()
    {
        health--;
        uiManager.UpdateLifes(health);
        if (health <= 0)
        {
            Die();
        }
    }

    public void SetHealthToZero()
    {
        health = 0;
        uiManager.UpdateLifes(health);
        Die();
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}