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



    private bool gameActive = false;


    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        //startButton = GetComponent<Button>();
        startButton.onClick.AddListener(StartGame);

        
    }

    // Update is called once per frame
    void Update()
    {

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        UpdateWaveText(spawnManagerScript.nextWave);
        UpdateHealth(playerControllerScript.healthCount);

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

    void EndGame()
    {
        spawnManagerScript.gameActive = false;
        startButton.gameObject.SetActive(true);
        //playerControllerScript.ResetHealth();
        spawnManagerScript.ResetNextWave();

    }


    //method to keep wave text up to date
    public void UpdateWaveText(int pWaveNo)
    {
        waveText.text = "Wave: " + pWaveNo;
    }

    //method to keep health text up to date
    public void UpdateHealth(int pHealthNo)
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

            //test with destroying enemies at end of game...
            foreach(GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            
        }
    }

}
