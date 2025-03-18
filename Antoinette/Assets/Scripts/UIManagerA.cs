using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerA : MonoBehaviour
{

    //[SerializeField]
    //private Image Lifes;
    public Image healthBarImage;
    public Sprite oneHeartSprite;
    public Sprite twoHeartsSprite;
    public Sprite threeHeartsSprite;
    [SerializeField]
    private Sprite[] LifesCountSprites;

    public void UpdateHealthUI(int currentHealth)
    {

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
    public void UpdateLifes(int currentHealth)
    {
        UpdateHealthUI(currentHealth);
    }
}
 
