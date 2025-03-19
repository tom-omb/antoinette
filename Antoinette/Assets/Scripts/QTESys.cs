using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class QTESys : MonoBehaviour
{
    public GameObject WKey;
    public GameObject EKey;
    public GameObject SKey;
    public GameObject key;

    public static int QTEKey = 4; // set to higher than # of keys so it doesnt accidentally activate
    public static bool passed = false;
    public static bool waitingForKey = false; // "is the QTE ongoing?"
    private int correctKey = 2;

    public GameObject Ant;

    void Update()
    {
        if (waitingForKey)
        {
            waitingForKey = false; //prevents looping
            Vector3 k_position = new Vector3(Ant.transform.position.x + 0.3f, Ant.transform.position.y + 0.3f, Ant.transform.position.z);

            if (QTEKey == 1)
            {
                key = Instantiate(WKey, k_position, Quaternion.identity);
            }
            else if (QTEKey == 2)
            {
                key = Instantiate(EKey, k_position, Quaternion.identity);
            }
            else if (QTEKey == 3)
            {
                key = Instantiate(SKey, k_position, Quaternion.identity);
            }
        }


        if (Input.anyKeyDown)
        {
            if (QTEKey == 1 && Input.GetButtonDown("Wkey"))
            {
                correctKey = 1;
            }
            else if (QTEKey == 2 && Input.GetButtonDown("Ekey"))
            {
                correctKey = 1;
            }
            else if (QTEKey == 3 && Input.GetButtonDown("Skey"))
            {
                correctKey = 1;
            }
            else
            {
                correctKey = 2;
            }

            StartCoroutine(KeyPressing());
            Destroy(key);

        }
    }

    IEnumerator KeyPressing()
    {
        QTEKey = 4;

        if (correctKey == 1)
        {
            Debug.Log("Passed");
            passed = true;
        }
        else if (correctKey == 2)
        {
            Debug.Log("Missed");
            passed = false;
        }

        yield return new WaitForSeconds(1.5f);
        correctKey = 2;
        yield return null;
    }
}
