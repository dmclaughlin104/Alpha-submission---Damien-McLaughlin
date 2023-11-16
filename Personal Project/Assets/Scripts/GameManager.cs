using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI gameOver1;
    public TextMeshProUGUI gameOver2;
    //private TextMeshProUGUI startMenu;
    private PlayerController playerControllerScript;
    private SpawnManager spawnManagerScript;


    public bool gameActive;


    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateWaveText(playerControllerScript.waveNumber);
        UpdateHealth(playerControllerScript.healthCount);

    }

    //method to start game...
    public void StartGame()
    {

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
            healthText.gameObject.SetActive(false);
            waveText.gameObject.SetActive(false);
            gameOver2.text = "You reached wave " + playerControllerScript.waveNumber;
            gameOver1.gameObject.SetActive(true);
            gameOver2.gameObject.SetActive(true);


        }
    }

}
