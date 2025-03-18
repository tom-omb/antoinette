using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Health UI")]
    public Image healthBarImage;  
    public Sprite oneHeartSprite; 
    public Sprite twoHeartsSprite; 
    public Sprite threeHeartsSprite; 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateLifes(int currentHealth)
{
    UpdateHealthUI(currentHealth);
}

    public void UpdateHealthUI(int currentHealth)
    {
        if (healthBarImage == null)
        {
            Debug.LogError("⚠️ HealthBar Image is not assigned in UIManager!");
            return;
        }

         //Change the sprite based on health value
        if (currentHealth == 1)
        {
            healthBarImage.sprite = oneHeartSprite;
        }
        else if (currentHealth == 2)
        {
            healthBarImage.sprite = twoHeartsSprite;
        }
        else if (currentHealth == 3)
        {
            healthBarImage.sprite = threeHeartsSprite;
        }
    }
}