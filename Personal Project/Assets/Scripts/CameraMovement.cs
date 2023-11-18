using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;
    private Vector3 distance = new Vector3(0.03f, 2.99f, -3.75f);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector3 back = player.transform.forward;
        back.y = -1f;
        
        // this determines how high. Increase for higher view angle.
        transform.position = player.transform.position - back * 5;

        transform.forward = player.transform.position - transform.position;

    }


}
