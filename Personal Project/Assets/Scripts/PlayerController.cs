using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //test with animator
    public Animator playerAnim;


    //variables
    [SerializeField] GameObject attackObject;
    [SerializeField] GameObject flames;
    [SerializeField] GameObject flames2;
    public GameObject grave;
    [SerializeField] GameObject damageIndicator;
    private SpawnManager spawnManagerScript;
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioClip slashSound;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip flamethrowerSound;
    [SerializeField] Slider flameThrowerSlider;//move to Game Manager?


    public EnemyController enemyControllerScript;

    public ParticleSystem slashParticle;

    private float forwardSpeed = 5.0f;
    private float turnSpeed = 150.0f;
    private float xBoundary = 8;
    private float zBoundary = 7;
    public bool hasPowerUp = false;
    private bool damageBufferWait = false;
    public int healthCount = 3;
    public int maxHealth = 3;
    private float flamethrowerTime = 4.0f;
    private float damageBufferTime = 2.5f;
    private bool isSlashing = false;


    // Start is called before the first frame update
    void Start()
    {

        //TEST
        //enemyControllerScript =  GameObject.Find("Weed Enemy").GetComponent<EnemyController>();

        //finding Spawn Manager in order to take wave number variable
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        //getting player audio
        playerAudio = GetComponent<AudioSource>();

        //setting value for flame-thrower slider
        flameThrowerSlider.maxValue = flamethrowerTime;

    }

    // Update is called once per frame
    void Update()
    {

        //flame-throwerUI counter
        //N.B this doesn't work as expected inside other methods
        //needs to be constantly called every frame to give gradual decreasing effect
        if (hasPowerUp == true)
        {
            FlameThrowerUI();
        }

        //if game is live, player can move
        if (spawnManagerScript.gameActive)
        {
            

            MovementControls();
            PlayerBoundaryControls();
        }

        //detect if player is attacking/slashing
        if (Input.GetKeyDown(KeyCode.Space) && !isSlashing && hasPowerUp == false && spawnManagerScript.gameActive)
        {
            //playing slash sound effect
            //here rather than in method in an attempt to reduce lag
            playerAudio.PlayOneShot (slashSound);

            SlashEffect();
            StartCoroutine(SlashEndCountdown());
        }


    }

    //method which activates player movement controls
    void MovementControls()
    {
        //player movement controls:
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        //test
        //Vector3 movement = this.transform.forward * forwardInput + this.transform.right * horizontalInput;
        //this.transform.position += movement * 0.05f;

        transform.Translate(Vector3.forward * forwardInput * forwardSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime);

        this.playerAnim.SetFloat("vertical", forwardInput);
        this.playerAnim.SetFloat("horizontal", horizontalInput);

    }

    void PlayerBoundaryControls()
    {
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
    }


    //a method to represent the slash movement with a stand in object
    void SlashEffect()
    {
        isSlashing = true;
        attackObject.SetActive(true);
        slashParticle.Play();
        this.playerAnim.SetBool("isSlashing", true);
    }

    //ends the slash attack
    //N.B. this won't work if included within single slash method
    //as the slash will remain active
    IEnumerator SlashEndCountdown()
    {
        yield return new WaitForSeconds(0.2f);
        attackObject.SetActive(false);
        this.playerAnim.SetBool("isSlashing", false);
        isSlashing = false;

    }

    //actions if triggers are activated
    private void OnTriggerEnter(Collider other)
    {
        //if player picks up power up, start flame-thrower
        if (other.CompareTag("PowerUp") && hasPowerUp == false)
        {
            Destroy(other.gameObject);//destroy power-up
            flames.SetActive(true);
            flames2.SetActive(true);
            flameThrowerSlider.value = flamethrowerTime;//setting UI back to full
            hasPowerUp = true;
            this.playerAnim.SetBool("isFlamethrowing", true);

            playerAudio.PlayOneShot(flamethrowerSound);
            StartCoroutine(FlamethrowerCountdown());
        }//if
        //or if player is struck by enemy
        else if (other.CompareTag("Weed Enemy") && damageBufferWait == false)
        {
            HealthDamage();
            //UnityEngine.Debug.Log("Buffer wait = " + damageBufferWait + " ...and should be True");
        }//else if
    }

    //method for depleting health
    void HealthDamage()
    {
        healthCount--;
        playerAudio.PlayOneShot(hurtSound);
        damageBufferWait = true;
        damageIndicator.gameObject.SetActive(true);
        StartCoroutine(DamageBufferCountdown());

        //if health is fully depleted, play deathbell sound
        if (healthCount == 0)
        {
            playerAudio.PlayOneShot(deathSound);
            
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
        damageIndicator.gameObject.SetActive(false);
        //UnityEngine.Debug.Log("Buffer wait = " + damageBufferWait + " ...and should be false");
    }


    //ends the flamethrower power-up
    IEnumerator FlamethrowerCountdown()
    {
        yield return new WaitForSeconds(flamethrowerTime);
        flames.SetActive(false);
        flames2.SetActive(false);
        hasPowerUp = false;
        this.playerAnim.SetBool("isFlamethrowing", false);
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
