using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerB : MonoBehaviour
{
    [SerializeField]
    private Image Life;
    [SerializeField]
    private Sprite[] LifeCountSprites;


    public void UpdateLifes1(int currentLives)
    {
        Life.sprite = LifeCountSprites[currentLives];
    }
}
