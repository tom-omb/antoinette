using System.Collections;
using UnityEngine;


public class AutoScrollEvents : MonoBehaviour
{
    public float LVL_speed = 1f; // preferably not higher than 2 cause it messes w the QTE
    public GameObject Bee_obj;
    public GameObject Ant_obj;
    private Rigidbody2D Bee_rb;

    private float offset;

    public GameObject Cactus_Prefab;
    public GameObject Spike_Prefab;
    public GameObject Cup_Prefab;
    public GameObject Spoon_Prefab;

    public static bool LevelFail = false;

    private GameObject obstacle1;
    private GameObject obstacle2;

    public static bool EnableInput = true; // effects X-axis movement and crouching
    public static bool disableJump = false;

    private bool firstpass = false;
    private bool secondpass = false;

    audioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Bee_rb = Bee_obj.GetComponent<Rigidbody2D>();
        offset = -1.5f;

        StartCoroutine(Events());
    }

    IEnumerator Events()
    {
        Autoscroll(true);

        yield return new WaitForSeconds(1f * LVL_speed);
        Bee_obj.GetComponent<BeetriceWingAttack>().StartWingAttack();

        yield return new WaitForSeconds(1.5f);
        ResetBeePosition();

        // ------------------------ CACTUS QUICK-TIME EVENT ------------------------
        ResetQTE();
        while ((!firstpass || !secondpass) && !LevelFail)
        {
            if (obstacle1 == null)
            {
                obstacle1 = Instantiate(Cactus_Prefab, new Vector3(Bee_obj.transform.position.x - offset, 1f, -1.2f), Quaternion.identity);
            }

            if (obstacle1 != null && Vector2.Distance(obstacle1.transform.position, Ant_obj.transform.position) <= 0.75f)
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
                        audioManager.playsoundef(audioManager.Soundef2);
                        GameObject obstacle2 = Instantiate(Spike_Prefab, new Vector3(obstacle1.transform.position.x, 1.2f, -1.2f), Quaternion.Euler(0, 0, -60));

                        float elapsedTime = 0f;
                        float attackTime = 1f;
                        while (elapsedTime < attackTime)
                        {
                            elapsedTime += Time.deltaTime;
                            obstacle2.transform.position = Vector3.Lerp(obstacle2.transform.position, Bee_obj.transform.position, elapsedTime / attackTime);
                            yield return null;
                        }

                        BeeHealth Beehealth = Bee_obj.GetComponent<BeeHealth>();
                        Beehealth.TakeDamage();
                        Destroy(obstacle2);
                        ResetBeePosition();
                        yield return new WaitForSeconds(0.75f);
                    }
                    else
                    {
                        // SECOND KEY MISSED
                        Ant_obj.GetComponent<AntHealth>().HealthLost();
                        yield return new WaitForSeconds(2f * LVL_speed);
                        Destroy(obstacle1);

                        FailCheck();
                        Bee_obj.GetComponent<BeetriceWingAttack>().StartWingAttack();
                        yield return new WaitForSeconds(1.5f);
                        QTEFail(); // nesting ienumerators is hard so I will be rewriting these three lines every time whoops
                        yield return new WaitForSeconds(0.75f);
                    }
                }
                else
                {
                    // FIRST KEY MISSED
                    Physics2D.IgnoreCollision(obstacle1.GetComponent<Collider2D>(), Ant_obj.GetComponent<Collider2D>());
                    yield return new WaitForSeconds(1f * LVL_speed);
                    Destroy(obstacle1);

                    FailCheck();
                    Bee_obj.GetComponent<BeetriceWingAttack>().StartWingAttack();
                    yield return new WaitForSeconds(1.5f);
                    QTEFail();
                    yield return new WaitForSeconds(0.75f);
                }
            }
            
            yield return null;
        }
        Autoscroll(true);

        yield return new WaitForSeconds(1f * LVL_speed);
        FailCheck();
        Bee_obj.GetComponent<BeetriceWingAttack>().StartWingAttack();
        yield return new WaitForSeconds(1.5f);
        ResetBeePosition();

        // ------------------------ CUP AND SPOON QUICK-TIME EVENT ------------------------
        ResetQTE();
        while ((!firstpass || !secondpass) && !LevelFail)
        {
            if (obstacle1 == null)
            {
                obstacle1 = Instantiate(Cup_Prefab, new Vector3(Bee_obj.transform.position.x - offset, 1f, -1.2f), Quaternion.identity);
                Vector2 spoonOffset = new Vector2(0.15f,0.265f);
                obstacle2 = Instantiate(Spoon_Prefab);
                obstacle2.transform.position = new Vector3(obstacle1.transform.position.x + spoonOffset.x, obstacle1.transform.position.y + spoonOffset.y, obstacle1.transform.position.z);
            }

            if (obstacle1 != null && Vector2.Distance(obstacle1.transform.position, Ant_obj.transform.position) <= 0.75f)
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
                        audioManager.playsoundef(audioManager.Soundef2);
                        float elapsedTime = 0f;
                        float attackTime = 1f;
                        while (elapsedTime < attackTime)
                        {
                            elapsedTime += Time.deltaTime;
                            obstacle2.transform.Rotate(10f, 0.0f, 0.0f, Space.Self);
                            obstacle2.transform.position = Vector3.Lerp(obstacle2.transform.position, Bee_obj.transform.position, elapsedTime / attackTime);
                            yield return null;
                        }

                        BeeHealth Beehealth = Bee_obj.GetComponent<BeeHealth>();
                        Beehealth.TakeDamage();
                        Destroy(obstacle2);
                        ResetBeePosition();
                        yield return new WaitForSeconds(0.75f);
                    }
                    else
                    {
                        // SECOND KEY MISSED
                        Ant_obj.GetComponent<AntHealth>().HealthLost();
                        yield return new WaitForSeconds(1f * LVL_speed);
                        Destroy(obstacle1);
                        Destroy(obstacle2);

                        FailCheck();
                        Bee_obj.GetComponent<BeetriceWingAttack>().StartWingAttack();
                        yield return new WaitForSeconds(1.5f);
                        QTEFail();
                        yield return new WaitForSeconds(0.75f);
                    }
                }
                else
                {
                    // FIRST KEY MISSED
                    Physics2D.IgnoreCollision(obstacle1.GetComponent<Collider2D>(), Ant_obj.GetComponent<Collider2D>());
                    yield return new WaitForSeconds(1f * LVL_speed);
                    Destroy(obstacle1);
                    Destroy(obstacle2);

                    FailCheck();
                    Bee_obj.GetComponent<BeetriceWingAttack>().StartWingAttack();
                    yield return new WaitForSeconds(1.5f);
                    QTEFail();
                    yield return new WaitForSeconds(0.75f);
                }
            }
            yield return null;
        }

        FailCheck();
        if (Bee_obj.GetComponent<BeeHealth>().isDefeated())
        {
            yield return new WaitForSeconds(0.25f);
            Autoscroll(false);
            EnableInput = true;
            yield return new WaitForSeconds(2f);
            Bee_obj.GetComponent<Rigidbody2D>().isKinematic = true;
        }

    }

    private void Autoscroll(bool scroll)
    {
        if (scroll)
        {
            EnableInput = false;
            disableJump = false;
            Bee_rb.velocity = new Vector2(0.5f * LVL_speed, 0);
            playerAnt.horizontalMovement = 0.5f * LVL_speed;
        }
        else
        {
            Bee_rb.velocity = Vector2.zero;
            playerAnt.horizontalMovement = 0f; // stop moving so that Ant attacks
        }
    }

    private void ResetBeePosition()
    {
        Vector3 BeePos = new Vector3(Ant_obj.transform.position.x - offset, 1.84f, -1.2f);
        Bee_obj.transform.position = BeePos; // re-orient bee in case one character moved too much
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
        obstacle1 = null;
        obstacle1 = null;
        LevelFail = false;
    }

    private void QTEFail()
    {
        ResetBeePosition();
        ResetQTE();
    }

    private void FailCheck()
    {
        if (LevelFail && !Bee_obj.GetComponent<BeeHealth>().isDefeated())
        {
            LevelFail = false;
            FAIL();
        }
    }


    public void FAIL()
    {
        StopAllCoroutines();

        if(obstacle1 != null)
        {
            Destroy(obstacle1);
        }
        if (obstacle2 != null)
        {
            Destroy(obstacle2);
        }

        Autoscroll(false);
        disableJump = true;
        Ant_obj.GetComponent<AntHealth>().LevelTwoFailed(); //not set to zero but a fail ui
    }
}