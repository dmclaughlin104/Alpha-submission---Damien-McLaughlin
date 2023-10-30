using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables
    public GameObject attackObject;
    public GameObject flames;
    public SpawnManager spawnManagerScript;
    public float forwardSpeed = 5.0f;
    private float turnSpeed = 150.0f;
    private float xBoundary = 8;
    private float zBoundary = 7;
    public bool hasPowerUp = false;
    private bool damageBufferWait = false;
    private int healthCount = 3;
    public int waveNumber;

    


    // Start is called before the first frame update
    void Start()
    {
        //finding Spawn Manager in order to take wave number variable
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //assigning wave number from Spawn Manager
        waveNumber = spawnManagerScript.nextWave;

        //printing current health to debug log
        if (healthCount > 0)
        {
            UnityEngine.Debug.Log("Your health = " + healthCount + "/3. You are on wave = " + waveNumber);
        }
        else
        {
            UnityEngine.Debug.Log("You've died - Game Over. You reached wave " + waveNumber);
        }
           
        //player movemebt controls:
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * forwardInput * forwardSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime);

        //if statement to control left player boundary
        if (transform.position.x < -xBoundary)
        {
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
        }//if

        //if statement to control right player boundary
        if (transform.position.x > xBoundary)
        {
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
        }//if

        //if statement to control player Z-axis upper boundary
        if (transform.position.z > zBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundary);
        }//if

        //if statement to control player Z-axis lower boundary
        if (transform.position.z < -zBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBoundary);
        }//if

        //detect if player is attacking/slashing
        if (Input.GetKeyDown(KeyCode.Space) && hasPowerUp == false)
        {
            SlashStandIn();
            StartCoroutine(SlashUndoCountdown());
        }

        //
        if (hasPowerUp == true)
        {
            flames.SetActive(true);
            StartCoroutine(FlamesUndoCountdown());
        }


    }

    //a method to represent the slash movement with stand in object
    void SlashStandIn()
    {
        attackObject.SetActive(true);
    }

    void SlashUndo()
    {
        attackObject.SetActive(false);
    }

    IEnumerator SlashUndoCountdown()
    {
        yield return new WaitForSeconds(0.2f);
        SlashUndo();
    }

    //checking if triggers are activated
    private void OnTriggerEnter(Collider other)
    {
        //if player picks up power up
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            flames.SetActive(true);
            hasPowerUp = true;
        }//if
        //or if player is struck by enemy
        else if (other.CompareTag("Enemy") && damageBufferWait == false)
        {
            HealthDown();
            //UnityEngine.Debug.Log("Buffer wait = " + damageBufferWait + " ...and should be True");

        }//else if
    }

    //method for depleting health
    void HealthDown()
    {
        healthCount--;
        damageBufferWait = true;
        StartCoroutine(DamageTakingCountdown());
    }
    

    //method to limit the amount of damage you can take in one skirmish

    void resetDamageBufferFalse()
    {
        damageBufferWait = false;
    }

    IEnumerator DamageTakingCountdown()
    {
        yield return new WaitForSeconds(2);
        resetDamageBufferFalse();
        //UnityEngine.Debug.Log("Buffer wait = " + damageBufferWait + " ...and should be false");
    }

    void FlamesUndo()
    {
        flames.SetActive(false);
    }
    IEnumerator FlamesUndoCountdown()
    {
        yield return new WaitForSeconds(4.0f);
        FlamesUndo();
        hasPowerUp = false;
    }


}   
