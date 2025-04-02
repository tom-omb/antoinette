using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;


public class AutoScrollEvents : MonoBehaviour
{
    public float LVL_speed = 1f; // preferably not higher than 2 cause it messes w the QTE
    public GameObject Bee_obj;
    public GameObject Ant_obj;
    private Rigidbody2D Bee_rb;

    private float offset;

    private Animator Bee_animator;

    public GameObject Cactus_Prefab;
    public GameObject Spike_Prefab;
    public GameObject Cup_Prefab;
    public GameObject Spoon_Prefab;

    public static bool EnableInput = true; // effects X-axis movement and crouching
    public static bool disableJump = false;

    private bool firstpass = false;
    private bool secondpass = false;
    private bool levelfail = false;


    // Start is called before the first frame update
    void Start()
    {
        Bee_animator = Bee_obj.GetComponent<Animator>();
        Bee_rb = Bee_obj.GetComponent<Rigidbody2D>();
        offset = Ant_obj.transform.position.x - Bee_obj.transform.position.x;

        StartCoroutine(Events());
    }

    private void Update()
    {
        levelfail = Ant_obj.transform.position.x >= 3f && !Bee_obj.GetComponent<BeeHealth>().isDefeated(); // if ant gets to the edge of the table and Bee isn't dead
        if (levelfail)
        {
            levelfail = false;
            StartCoroutine(LevelFAIL()); // this will stop both characters, make Bee attack without allowing Ant to dodge, and set health to zero.
        }
    }

    IEnumerator Events()
    {
        Autoscroll(true);

        yield return new WaitForSeconds(1f * LVL_speed);
        Bee_obj.GetComponent<BeetriceWingAttack>().StartWingAttack();

        yield return new WaitForSeconds(1.5f);
        ResetBeePosition();

        // ------------------------ CACTUS QUICK-TIME EVENT ------------------------
        GameObject cactus = null;
        while (!firstpass || !secondpass)
        {
            if (cactus == null)
            {
                cactus = Instantiate(Cactus_Prefab, new Vector3(Bee_obj.transform.position.x - offset, 1f, -1.2f), Quaternion.identity);
            }

            if (cactus != null && Vector2.Distance(cactus.transform.position, Ant_obj.transform.position) <= 0.75f)
            {
                StartQTE(1);
                yield return new WaitForSeconds(0.75f);  // wait for player's input
                firstpass = QTESys.passed; // store result

                if (firstpass) // only continue to E if W was passed
                {
                    StartQTE(2);
                    yield return new WaitForSeconds(0.75f);
                    secondpass = QTESys.passed;

                    if (secondpass)
                    {
                        Autoscroll(false); // stop moving so that Ant attacks
                        GameObject spike = Instantiate(Spike_Prefab, new Vector3(cactus.transform.position.x, 1.2f, -1.2f), Quaternion.Euler(0, 0, -60));

                        float elapsedTime = 0f;
                        float attackTime = 1f;
                        while (elapsedTime < attackTime)
                        {
                            elapsedTime += Time.deltaTime;
                            spike.transform.position = Vector3.Lerp(spike.transform.position, Bee_obj.transform.position, elapsedTime / attackTime);
                            yield return null;
                        }

                        BeeHealth Beehealth = Bee_obj.GetComponent<BeeHealth>();
                        Beehealth.TakeDamage();
                        Destroy(spike);
                        ResetBeePosition();
                    }
                    else
                    {
                        // SECOND KEY MISSED
                        Ant_obj.GetComponent<AntHealth>().HealthLost();
                        yield return new WaitForSeconds(2f * LVL_speed);
                        Destroy(cactus);

                        Bee_obj.GetComponent<BeetriceWingAttack>().StartWingAttack();
                        yield return new WaitForSeconds(1.5f);
                        ResetBeePosition(); // nesting ienumerators is hard so I will be rewriting these three lines every time whoops
                        ResetQTE();
                    }
                }
                else
                {
                    // FIRST KEY MISSED
                    Physics2D.IgnoreCollision(cactus.GetComponent<Collider2D>(), Ant_obj.GetComponent<Collider2D>());
                    yield return new WaitForSeconds(1f * LVL_speed);
                    Destroy(cactus);

                    Bee_obj.GetComponent<BeetriceWingAttack>().StartWingAttack();
                    yield return new WaitForSeconds(1.5f);
                    ResetBeePosition();
                    ResetQTE();
                }
            }
            yield return null;
        }
        Autoscroll(true);

        yield return new WaitForSeconds(1f * LVL_speed);
        Bee_obj.GetComponent<BeetriceWingAttack>().StartWingAttack();
        yield return new WaitForSeconds(1.5f);
        ResetBeePosition();

        // ------------------------ CUP AND SPOON QUICK-TIME EVENT ------------------------
        GameObject cup = null;
        GameObject spoon = null;
        ResetQTE();
        while (!firstpass || !secondpass)
        {
            if (cup == null)
            {
                cup = Instantiate(Cup_Prefab, new Vector3(Bee_obj.transform.position.x - offset, 1f, -1.2f), Quaternion.identity);
                Vector2 spoonOffset = new Vector2(0.15f,0.265f);
                spoon = Instantiate(Spoon_Prefab);
                spoon.transform.position = new Vector3(cup.transform.position.x + spoonOffset.x, cup.transform.position.y + spoonOffset.y, cup.transform.position.z);
            }

            if (cup != null && Vector2.Distance(cup.transform.position, Ant_obj.transform.position) <= 0.75f)
            {
                StartQTE(1);
                yield return new WaitForSeconds(0.75f);  // wait for player's input
                firstpass = QTESys.passed; // store result

                if (firstpass) // only continue to E if W was passed
                {
                    StartQTE(3);
                    yield return new WaitForSeconds(0.75f);
                    secondpass = QTESys.passed;

                    if (secondpass)
                    {
                        Autoscroll(false); // stop moving so that Ant attacks

                        float elapsedTime = 0f;
                        float attackTime = 1f;
                        while (elapsedTime < attackTime)
                        {
                            elapsedTime += Time.deltaTime;
                            spoon.transform.Rotate(10f, 0.0f, 0.0f, Space.Self);
                            spoon.transform.position = Vector3.Lerp(spoon.transform.position, Bee_obj.transform.position, elapsedTime / attackTime);
                            yield return null;
                        }

                        BeeHealth Beehealth = Bee_obj.GetComponent<BeeHealth>();
                        Beehealth.TakeDamage();
                        Destroy(spoon);
                        ResetBeePosition();
                    }
                    else
                    {
                        // SECOND KEY MISSED
                        Ant_obj.GetComponent<AntHealth>().HealthLost();
                        yield return new WaitForSeconds(1f * LVL_speed);
                        Destroy(cup);

                        Bee_obj.GetComponent<BeetriceWingAttack>().StartWingAttack();
                        yield return new WaitForSeconds(1.5f);
                        ResetBeePosition();
                    }
                }
                else
                {
                    // FIRST KEY MISSED
                    Physics2D.IgnoreCollision(cup.GetComponent<Collider2D>(), Ant_obj.GetComponent<Collider2D>());
                    yield return new WaitForSeconds(1f * LVL_speed);
                    Destroy(cup);

                    Bee_obj.GetComponent<BeetriceWingAttack>().StartWingAttack();
                    yield return new WaitForSeconds(1.5f);
                    ResetBeePosition();
                }
            }
            yield return null;
        }

        if (Bee_obj.GetComponent<BeeHealth>().isDefeated())
        {
            Autoscroll(false);
            EnableInput = true;
            yield return new WaitForSeconds(1.5f);
            Bee_obj.GetComponent<Rigidbody2D>().isKinematic = true;
        }

    }

    private void Autoscroll(bool scroll)
    {
        if (scroll)
        {
            EnableInput = false;
            Bee_rb.velocity = new Vector2(0.5f * LVL_speed, 0);
            playerAnt.horizontalMovement = 0.5f * LVL_speed;
        }
        else
        {
            Bee_rb.velocity = new Vector2(0, 0);
            playerAnt.horizontalMovement = 0f; // stop moving so that Ant attacks
        }
    }

    private void ResetBeePosition()
    {
        Vector3 BeePos = new Vector3(Ant_obj.transform.position.x - offset, 1.84f, -1.2f);
        Bee_obj.transform.position = BeePos;
        // re-orient bee in case one character moved too much
        Autoscroll(true);
    }

    private void StartQTE(int k)
    {
        QTESys.QTEKey = k; // sets up input
        QTESys.waitingForKey = true; // starts QTE
    }

    private void ResetQTE()
    {
        firstpass = false;
        secondpass = false;
    }

    IEnumerator LevelFAIL()
    {
        disableJump = true;
        Autoscroll(false);
        Bee_obj.GetComponent<BeetriceWingAttack>().StartWingAttack();
        yield return new WaitForSeconds(0.75f);
        Ant_obj.GetComponent<AntHealth>().SetHealthToZero();
        yield break;
    }
}
