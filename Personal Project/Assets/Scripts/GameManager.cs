using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI gameOver1;
    public TextMeshProUGUI gameOver2;
    private PlayerController playerControllerScript;
    private SpawnManager spawnManagerScript;
    public Button startButton;


    public bool gameActive;


    // Start is called before the first frame update
    void Start()
    {
        //getting access to other scripts
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        //start/restart button
        startButton = GetComponent<Button>();



    }

    // Update is called once per frame
    void Update()
    {

        StartScreenMenu();
        
        UpdateWaveText(playerControllerScript.waveNumber);
        UpdateHealth(playerControllerScript.healthCount);

    }



    public void StartScreenMenu()
    {
        gameActive = false;
        titleText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);

        if (gameActive == false)
        {
            StartScreenMenu();
            startButton.onClick.AddListener(StartGame);
        }

    }


    //method to start game...
    public void StartGame()
    {
        //lifting from Spawn manager - this needs improved...
        //spawnManagerScript.StartGameplay();

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
            GameOverScreen();
        }
    }

    public void GameOverScreen()
    {
        healthText.gameObject.SetActive(false);
        waveText.gameObject.SetActive(false);
        gameOver2.text = "You reached wave " + playerControllerScript.waveNumber;
        gameOver1.gameObject.SetActive(true);
        gameOver2.gameObject.SetActive(true);
    }

}
