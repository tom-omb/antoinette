using System.Collections;
using System.Runtime.CompilerServices;
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
    // public GameObject Cup_Prefab;
    // public GameObject Spoon_Prefab;

    public static bool EnableInput = true;
    private bool firstpass = false;
    private bool secondpass = false;
    private bool qte_end = false;


    // Start is called before the first frame update
    void Start()
    {
        Bee_animator = Bee_obj.GetComponent<Animator>();
        Bee_rb = Bee_obj.GetComponent<Rigidbody2D>();
        offset = Ant_obj.transform.position.x - Bee_obj.transform.position.x;

        StartCoroutine(Scrolling());
    }

    IEnumerator Scrolling()
    {
        EnableInput = false;
        Bee_rb.velocity = new Vector2(0.5f * LVL_speed, 0);
        playerAnt.horizontalMovement = 0.5f * LVL_speed;

        yield return new WaitForSeconds(1f * LVL_speed);
        StartCoroutine(BeeAttack());

        GameObject cactus = null;
        while (!firstpass && !secondpass && !qte_end)
        {
            if (cactus == null)
            {
                cactus = Instantiate(Cactus_Prefab, new Vector3(Bee_obj.transform.position.x - offset, 1f, -1.2f), Quaternion.identity);
            }

            if (cactus != null && Vector2.Distance(cactus.transform.position, Ant_obj.transform.position) <= 0.75f)
            {
                QTESys.QTEKey = 1; // sets up W input
                QTESys.waitingForKey = true; // starts QTE
                yield return new WaitForSeconds(0.75f);  // wait for player's input

                firstpass = QTESys.passed; // store result
                if (firstpass) // only continue to E if W was passed
                {
                    QTESys.QTEKey = 2; 
                    QTESys.waitingForKey = true;
                    yield return new WaitForSeconds(0.75f);

                    secondpass = QTESys.passed;
                    if (QTESys.key == null) // this was more consistent w getting second QTE result idk
                    {
                        Bee_rb.velocity = new Vector2(0, 0);
                        playerAnt.horizontalMovement = 0f; // stop moving so that Ant attacks
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
                    }
                    else
                    {
                        // SECOND KEY MISSED
                        Ant_obj.GetComponent<AntHealth>().HealthLost();
                        qte_end = true;
                    }
                }
                else
                {
                    // FIRST KEY MISSED
                    Physics2D.IgnoreCollision(cactus.GetComponent<Collider2D>(), Ant_obj.GetComponent<Collider2D>());
                    qte_end = true;
                }
            }
            yield return null;
        }

        Bee_rb.velocity = new Vector2(0.5f * LVL_speed, 0);
        playerAnt.horizontalMovement = 0.5f * LVL_speed;

        yield return new WaitForSeconds(1f * LVL_speed);
        StartCoroutine(BeeAttack());
    }

    IEnumerator BeeAttack()
    {
        Bee_obj.GetComponent<BeetriceWingAttack>().StartWingAttack();
        yield return new WaitForSeconds(1.5f);
        Bee_obj.GetComponent<BeetriceWingAttack>().StopAllCoroutines(); // end wing attack

        Bee_animator.SetBool("isAttacking", false);
        Bee_obj.transform.position = new Vector3(Ant_obj.transform.position.x - offset, 1.84f, -1.2f); // return Bee to her place
        Bee_rb.velocity = new Vector2(0.5f * LVL_speed, 0);
        yield break;
    }


}
