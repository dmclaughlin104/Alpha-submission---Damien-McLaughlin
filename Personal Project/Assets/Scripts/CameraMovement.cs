using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //NOTE - not currently being used
        //camera is currently attached to the player
        transform.position = player.transform.position + new Vector3(7.3f, 4, -3.3f);
        

    }
}
