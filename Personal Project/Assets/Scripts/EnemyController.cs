using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{


    //test with animator
    public Animator enemyAnim;

    private Rigidbody enemyRB;

    public GameObject smokeParticle;
    public GameObject weedBloodParticle;


    public GameObject[] enemyBodyParts;


    //variables
    public string playerTag = "Player";
    private float movementSpeed = 1f;
    private bool isDead = false;
    private float attackForce = 1.5f;
    Vector3 moveDirection;


    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        //getting weed animator
        enemyAnim = GetComponent<Animator>();

        enemyRB = GetComponent<Rigidbody>();

        // Find the player object using the tag
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    // Update is called once per frame
    void Update()
    {

        //controlling enemy movement, as long as enemy isn't dead
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
        moveDirection = (player.position - transform.position).normalized;

        // Move the enemy towards the player
        transform.Translate(moveDirection * movementSpeed * Time.deltaTime, Space.World);
    }

    //destroy enemies if an attack is being used when a collision occurs
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Slash"))
        {
            StartCoroutine(fogDelay());
            EnemyDeath();
            //test with pushing enemy back on slash attack
            enemyRB.AddForce(-moveDirection * attackForce, ForceMode.Impulse);
            Destroy(gameObject, 2);
        }
        else if (other.CompareTag("Flames"))
        {
            StartCoroutine(SmokeDelay());
            EnemyDeath();

            ChangeWeedMaterialBlack();
            
            Destroy(gameObject, 4f);
            
        }
    }


    void EnemyDeath()
    {
        isDead = true;//stopping enemy movement
        this.enemyAnim.SetBool("isDead", true);
        transform.gameObject.tag = "Dead Enemy";//changing tag so enemy doesn't cause health damage after dying
        

    }

    //method for changing key parts of enemy to black
    void ChangeWeedMaterialBlack()
    {

        foreach (GameObject bodyPart in enemyBodyParts)
        {
            SkinnedMeshRenderer bodyPartMesh = bodyPart.GetComponent<SkinnedMeshRenderer>();

            bodyPartMesh.material.color = Color.black;

        }
    }


    //waits until enemy is settled on ground before smoke effect starts
    public IEnumerator SmokeDelay()
    {
        yield return new WaitForSeconds(0.9f);
        smokeParticle.SetActive(true);
    }

    public IEnumerator fogDelay()
    {
        yield return new WaitForSeconds(0.25f);
        weedBloodParticle.SetActive(true);
    }

    //waits until enemy is settled on ground before smoke effect starts
    public IEnumerator EnemySinkDelay()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Translate(Vector3.down * 10f * Time.deltaTime);
    }

    //TEST - imagining that enemy is always moving
    /*
    void EnemyAnimationControls()
    {

        this.enemyAnim.SetFloat("vertical", 1);
        this.enemyAnim.SetFloat("horizontal", 1);
    }
    */

}
