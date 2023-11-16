using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI healthText;
    //private TextMeshProUGUI startMenu;
    private PlayerController playerControllerScript;
    public int waveNo;


    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateWaveText(playerControllerScript.waveNumber);
        UpdateHealth(playerControllerScript.healthCount);

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
            healthText.text = "You've died - Game Over. You reached wave " + playerControllerScript.waveNumber;
        }
    }

}
