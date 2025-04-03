using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightBeamRotator : MonoBehaviour
{
    public float interval = 2f; // one full movement in one direction, a full back-and-forth cycle takes 2 × interval seconds

    private Quaternion startRotation;
    private Quaternion endRotation;
    private float timer = 0f;
    private bool rotatingForward = true;

    // Start is called before the first frame update
    private void Start()
    {
        // 1. read 
        startRotation = transform.rotation; 
        //2. set A new rotation "z-axis only"
        endRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 90f); // 90 z-degrees
    }

    void Update()
    {
        timer += Time.deltaTime / interval; // starting from 0 on each update, 1 = full => reset.

        if (rotatingForward)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, timer);
            // Quaternion.Lerp(startRotation, targetRotation, t)
            // start: returned when t = 0.
            // end:   returned when t = 1.
            // t :    The value is clamped to the range [0, 1], controlling the transition
        }
        else
        {
            transform.rotation = Quaternion.Lerp(endRotation, startRotation, timer);
        }

        if (timer >= 1f)                        // full cycle is complete
        {
            timer = 0f;                         // reset timer to start the other direction
            rotatingForward = !rotatingForward; // rotating Backwards ~
        }
    }
}
