using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //variables
    [SerializeField] GameObject attackObject;
    [SerializeField] GameObject flames;
    private SpawnManager spawnManagerScript;
    public DamageFlash damageFlashScript;
    [SerializeField] AudioSource playerAudio;
    [SerializeField] Slider flameThrowerSlider; //move to Game Manager?


    private float forwardSpeed = 5.0f;
    private float turnSpeed = 150.0f;
    private float xBoundary = 8;
    private float zBoundary = 7;
    private bool hasPowerUp = false;
    private bool damageBufferWait = false;
    private bool flashing = false;
    public int healthCount = 3;
    public int maxHealth = 3;
    private float flamethrowerTime = 4.0f;
    private float damageBufferTime = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        //finding Spawn Manager in order to take wave number variable
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        //finding Damage Flash Script
        damageFlashScript = GameObject.Find("Damage Flash Capsule").GetComponent<DamageFlash>();

        //getting player audio
        playerAudio = GetComponent<AudioSource>();

        //setting value for flame-thrower slider
        flameThrowerSlider.maxValue = flamethrowerTime;

    }

    // Update is called once per frame
    void Update()
    {


        //flame-throwerUI
        if (hasPowerUp == true)
        {

            FlameThrowerUI();
        }


        //player movement controls:
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
            //playing slash sound effect
            //(here rather than in method in an attempt to reduce lag)
            playerAudio.Play();
            SlashEffect();
            StartCoroutine(SlashEndCountdown());
        }

        //TESTING WITH DAMAGE FLASH
        if (damageBufferWait)
        {
            InvokeRepeating("DamageFlash2", 0f, 0.5f);
        }
        else
        {
            CancelInvoke("DamageFlash2");
        }



    }



    //a method to represent the slash movement with stand in object
    void SlashEffect()
    {
        attackObject.SetActive(true);
        
    }

    //ends the slash attack
    //N.B. this won't work if included within single slash method
    //as the slash will remain active
    IEnumerator SlashEndCountdown()
    {
        yield return new WaitForSeconds(0.2f);
        attackObject.SetActive(false);
    }

    //actions if triggers are activated
    private void OnTriggerEnter(Collider other)
    {
        //if player picks up power up, start flame-thrower
        if (other.CompareTag("PowerUp") && hasPowerUp == false)
        {
            Destroy(other.gameObject);
            flames.SetActive(true);
            flameThrowerSlider.value = flamethrowerTime;//setting UI back to full
            hasPowerUp = true;
            StartCoroutine(FlamethrowerCountdown());

        }//if
        //or if player is struck by enemy
        else if (other.CompareTag("Enemy") && damageBufferWait == false)
        {
            HealthDamage();
            //UnityEngine.Debug.Log("Buffer wait = " + damageBufferWait + " ...and should be True");
        }//else if
    }

    //method for depleting health
    void HealthDamage()
    {
        healthCount--;
        damageBufferWait = true;
        //DamageFlash1();//TESTING
        StartCoroutine(DamageBufferCountdown());
    }

    //TESTING WITH DAMAGE FLASH INDICATOR
    void DamageFlash1()
    {
        if (damageBufferWait)
        {
            InvokeRepeating("DamageFlash2", 0.15f, 0.3f);
        }

    }

    void DamageFlash2()
    {

        for (float repeatTime = 2.5f; repeatTime > 0; repeatTime -= Time.deltaTime)
        {
            damageFlashScript.FlashStart();
        }

    }


    //method to reset health to full
    public void ResetHealth()
    {
        healthCount = maxHealth;
    }


    
    //damage buffer method to limit the amount of health you can lose in a short period
    IEnumerator DamageBufferCountdown()
    {
        yield return new WaitForSeconds(damageBufferTime);
        damageBufferWait = false;
        //UnityEngine.Debug.Log("Buffer wait = " + damageBufferWait + " ...and should be false");
    }

    IEnumerator DamageFlashCountdown()
    {
        yield return new WaitForSeconds(0.3f);
        
    }

    //ends the flamethrower power-up
    IEnumerator FlamethrowerCountdown()
    {
        yield return new WaitForSeconds(flamethrowerTime);
        flames.SetActive(false);
        hasPowerUp = false;
    }


    //attempt at having a flamethrower timer UI
    //note - max value set in start method... does this need changed?
    void FlameThrowerUI()
    {

        float time = Time.deltaTime;

        flameThrowerSlider.value -= 1 * time;

    }



    //method for debugging
    void PrintHealthToDebugLog()
    {
        //printing current health to debug log
        if (healthCount > 0)
        {
            UnityEngine.Debug.Log("Your health = " + healthCount + "/3. You are on wave = " + spawnManagerScript.nextWave);
        }
        else
        {
            UnityEngine.Debug.Log("You've died - Game Over. You reached wave " + spawnManagerScript.nextWave);
        }
    }


}   
