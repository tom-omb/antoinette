using UnityEngine;

public class BeeFollow_TEMPORARY : MonoBehaviour
{

    float x_offset;
    public GameObject player;


    void Start()
    {
        x_offset = player.transform.position.x - transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x - x_offset, transform.position.y, transform.position.z);
    }
}