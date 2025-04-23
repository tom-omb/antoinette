using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sugar_interact : MonoBehaviour
{
    public GameObject Ant;
    public GameObject Ekey_prefab;
    private GameObject Ekey;



    // Start is called before the first frame update
    void Start()
    {
        Ekey = Instantiate(Ekey_prefab, new Vector3(this.transform.position.x - 0.3f, this.transform.position.y + 0.5f, this.transform.position.z), Quaternion.identity);
        Ekey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(this.transform.position, Ant.transform.position) <= 0.75f)
        {
            Ekey.SetActive(true);
            if (Input.GetButtonDown("Ekey"))
            {
                CutsceneManager.currentScene = 1;
                SceneManager.LoadScene("cutscenes");
                Time.timeScale = 1f; ;
            }
        } 
        else if (Vector2.Distance(this.transform.position, Ant.transform.position) > 0.75f)
        {
            Ekey.SetActive(false);
        }
    }
}
