using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{

    public Transform player; // Reference to the player's transform
    public Vector3 offset = new Vector3(0.03f, 2.99f, -3.75f); // Adjust this to set the camera offset

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            // Set the camera position to the player's position plus the offset
            transform.position = player.position + offset;

            //transform.LookAt(player);
        }


    }
}
