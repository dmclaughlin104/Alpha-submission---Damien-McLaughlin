using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI titleScreen;
    public Button startButton;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI gameOver1;
    public TextMeshProUGUI gameOver2;
    private PlayerController playerControllerScript;
    private SpawnManager spawnManagerScript;
    private GameObject[] enemies;



    // Start is called before the first frame update
    void Start()
    {
        //finding scripts
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        //adding listener to button
        startButton.onClick.AddListener(StartGame);


    }

    // Update is called once per frame
    void Update()
    {
        //finding enemies for array
        //needs to be in update to keep up to date
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        UpdateWaveText(spawnManagerScript.nextWave);
        HealthManager(playerControllerScript.healthCount);

    }

    //method to start game...
    void StartGame()
    {
        //Debug.Log("Button clicked");

        healthText.gameObject.SetActive(true);
        waveText.gameObject.SetActive(true);

        gameOver1.gameObject.SetActive(false);
        gameOver2.gameObject.SetActive(false);

        if (playerControllerScript.healthCount > 0)
        {
            spawnManagerScript.gameActive = true;

            //setting title screen/button inactive when gameplay begins
            titleScreen.gameObject.SetActive(false);
            startButton.gameObject.SetActive(false);

        }
        
 
    }

    //method to set game as inactive and reset key elements for next play
    void EndGame()
    {
        spawnManagerScript.gameActive = false;
        startButton.gameObject.SetActive(true);
        playerControllerScript.ResetHealth();
        spawnManagerScript.ResetNextWave();

        //destroying all remaining enemies at the end of the game
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }


    //method to keep wave text up to date
    public void UpdateWaveText(int pWaveNo)
    {
        waveText.text = "Wave: " + pWaveNo;
    }

    //method to keep health text up to date
    public void HealthManager(int pHealthNo)
    {
        if (playerControllerScript.healthCount > 0)
        {
            healthText.text = "Health: " + pHealthNo + "/3";
        }
        else
        {
            int gameOverWaveNumber = spawnManagerScript.nextWave;
            healthText.gameObject.SetActive(false);
            waveText.gameObject.SetActive(false);
            gameOver2.text = "You reached wave " + gameOverWaveNumber;
            gameOver1.gameObject.SetActive(true);
            gameOver2.gameObject.SetActive(true);
            EndGame();

        }
    }

}
