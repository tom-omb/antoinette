using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class bee_interact : MonoBehaviour
{
    public GameObject Ant;
    public GameObject Ekey_prefab;
    private GameObject Ekey;

    public GameObject contBtn;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;

    public TextMeshProUGUI names;
    public Image talking;
    public Sprite[] ant_sprites;
    public Sprite[] bee_sprites;
    private bool BeeTalking;
    private bool AntTalking;

    public float wordSpeed;

    void Start()
    {
        Ekey = Instantiate(Ekey_prefab, Vector3.zero, Quaternion.identity);
        Ekey.SetActive(false);
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<BeeHealth>().isDefeated())
        {
            if (Vector2.Distance(this.transform.position, Ant.transform.position) <= 0.75f)
            {
                Ekey.transform.position = new Vector3(this.transform.position.x - 0.3f, this.transform.position.y + 0.3f, this.transform.position.z);
                Ekey.SetActive(true);

                if (Input.GetButtonDown("Ekey"))
                {
                    if (dialoguePanel.activeInHierarchy)
                    {
                        ZeroText();
                    }
                    else
                    {
                        dialoguePanel.SetActive(true);
                        StartCoroutine(Typing());
                    }
                }
            }
            else if (Vector2.Distance(this.transform.position, Ant.transform.position) > 0.75f)
            {
                Ekey.SetActive(false);
                ZeroText();
            }

            if (!dialoguePanel.activeInHierarchy)
            {
                AutoScrollEvents.EnableInput = true;
                AutoScrollEvents.disableJump = false;
                StopAllCoroutines();
            }

            if (dialogueText.text == dialogue[index])
            {
                contBtn.SetActive(true);
            }


            if (dialoguePanel.activeInHierarchy)
            {
                AutoScrollEvents.EnableInput = false;
                AutoScrollEvents.disableJump = true;
            }
        }
    }

    public void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        switchSprites();
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contBtn.SetActive(false);
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else if (index == dialogue.Length - 1)
        {
            CutsceneManager.currentScene = 2;
            SceneManager.LoadScene("cutscenes");
            Time.timeScale = 1f; ;
        }
        else
        {
            ZeroText();
        }

        
    }

    public void switchSprites()
    {
        //bee lines @ index = 0,2,5,8,10
        //ant lines @ index = 1,3,4,6,7,9

        if (index == 0 || index == 2 || index == 5 || index == 8 || index == 10)
        {
            BeeTalking = true;
            AntTalking = false;
        }
        else
        {
            BeeTalking = false;
            AntTalking = true;
        }

        if (AntTalking)
        {
            names.text = "Antoinette";
            if (index == 3 || index == 6)
            {
                talking.sprite = ant_sprites[1];
            }
            else
            {
                talking.sprite = ant_sprites[0];
            }
        }
        else if (BeeTalking)
        {
            names.text = "Beetrice";
            if (index == 2 || index == 8)
            {
                talking.sprite = bee_sprites[1];
            }
            else if (index == 5)
            {
                talking.sprite = bee_sprites[2];
            }
            else
            {
                talking.sprite = bee_sprites[0];
            }
        }
    }
}
