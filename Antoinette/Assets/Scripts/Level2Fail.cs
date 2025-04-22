using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Fail : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        AutoScrollEvents.LevelFail = true;
    }
}
