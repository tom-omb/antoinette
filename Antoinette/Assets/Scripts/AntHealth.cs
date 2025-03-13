using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 1;
    

    public void HealthGained()
    {
        health++;
    }
}
