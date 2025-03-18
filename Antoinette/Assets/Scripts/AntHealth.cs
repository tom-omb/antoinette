using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AntHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 3;
    private Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void HealthGained()
    {
        health++;
        if (health > 3)
        {
            health = 3;
        } // make sure she doesn't exceed max health
    }

    public void HealthLost()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
    }
    public void SetHealthToZero()
    {
        health = 0;
        Die();
    }
 
    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
