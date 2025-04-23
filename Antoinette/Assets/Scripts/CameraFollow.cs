using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    Vector3 offset;
    Vector3 cameraPos;
    //We need the camera to be a constant distance away from the player
    //So this offset will have the value of (player position-camera position)

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        offset = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos = player.transform.position - offset;
        transform.position = cameraPos;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0, 4.77f), transform.position.z);
    }
}
