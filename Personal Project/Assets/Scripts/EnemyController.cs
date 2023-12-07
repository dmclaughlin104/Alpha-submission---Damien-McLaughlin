using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    //test with animator
    public Animator enemyAnim;

    //variables
    public string playerTag = "Player";
    private float movementSpeed = 1f;
    public bool isDead = false;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        //getting weed animator
        enemyAnim = GetComponent<Animator>();

        // Find the player object using the tag
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isDead)
        {
            // Look at the player
            LookAtPlayer();

            // Move toward the player
            MoveTowardsPlayer();
        }

        //TEST
        //EnemyAnimationControls();

    }

    void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void MoveTowardsPlayer()
    {
        // Calculate the movement direction towards the player
        Vector3 moveDirection = (player.position - transform.position).normalized;

        // Move the enemy towards the player
        transform.Translate(moveDirection * movementSpeed * Time.deltaTime, Space.World);
    }

    //destroy enemies if an attack is being used when a collision occurs
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Attack"))
        {
            isDead = true;
            this.enemyAnim.SetBool("isDead", true);
            transform.gameObject.tag = "Dead Enemy";//changing tag so enemy doesn't cause health damage after dying
            
            Destroy(gameObject, 2);
        }
    }

    /*
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    */

    //TEST - imagining that enemy is always moving
    /*
    void EnemyAnimationControls()
    {

        this.enemyAnim.SetFloat("vertical", 1);
        this.enemyAnim.SetFloat("horizontal", 1);
    }
    */

}
