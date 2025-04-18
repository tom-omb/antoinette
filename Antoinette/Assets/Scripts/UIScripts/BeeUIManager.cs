using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeUIManager : MonoBehaviour
{
    [SerializeField]
    private Image Life;
    [SerializeField]
    private Sprite[] LifeCountSprites;


    public void UpdateBeeLifes(int currentLives) //was UpdateLifes1
    {
        Life.sprite = LifeCountSprites[currentLives];
    }
}
