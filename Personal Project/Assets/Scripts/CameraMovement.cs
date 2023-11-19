using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        //this was the result of some googling. It is working, but I'd like to research further
        Vector3 back = player.transform.forward;
        back.y = -1f;
        
        // this determines how high. Increase for higher view angle.
        transform.position = player.transform.position - back * 5;

        transform.forward = player.transform.position - transform.position;

    }


}
