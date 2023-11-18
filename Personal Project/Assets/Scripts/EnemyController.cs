using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //variables
    private GameObject player;
    private float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");//assigning Player to variable
    }

    // Update is called once per frame
    void Update()
    {
        //determining direction toward player
        Vector3 lookDirection = ((player.transform.position + new Vector3(0, 1, 0)) - transform.position).normalized;

        //moving enemy toward player
        transform.Translate(lookDirection *  speed * Time.deltaTime);

        //rotating enemy to face player - not sure if this is working...
        transform.LookAt(transform.position);


    }


    //destroy enemies if an attack is being used when a collision occurs
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            Destroy(gameObject);
        }
    }

}
