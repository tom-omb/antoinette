using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AntHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 1;
    

    public void HealthGained()
    {
        health++;
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
