using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //variables
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    private float spawnRange = 7;
    public int enemyCount;
    public int nextWave;

    public bool gameActive = false;


    // Start is called before the first frame update
    void Start()
    {
        //this is needed at present - move into Game Manager?
        //SpawnEnemyWave(nextWave);
    }

    // Update is called once per frame
    void Update()
    {
        //setting enemy count - N.B. using the scripts applied, not the tag
        enemyCount = FindObjectsOfType<EnemyController>().Length;

        //starting to call waves of enenmies... - move this to Game Manager?
        // seem to be starting on wave two now... why?
        
        
        if (gameActive == true)
        {
            StartGameplay();
        }
        


    }


    public void StartGameplay()
    {

        //if number spawning doesn't seem right, check int value in Unity...


        //if all enemies are defeated, spawn more
        if ((enemyCount == 0))
        {
            nextWave++;//increase number in wave
            SpawnEnemyWave(nextWave);

            //spawn a power up every even-numbered wave (over 4)
            if (nextWave >= 4 && ((nextWave % 2) == 0))
            {
                SpawnPowerUp();
            }//if

        }//if
    }

    //spawn an enemy wave
    void SpawnEnemyWave(int pWaveNumber)
    {
        for (int count = 0; count < pWaveNumber; count++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
        }
    }


    //method to spawn power up
    public void SpawnPowerUp()
    {
        Instantiate(powerUpPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
    }


    //method to generate a new random spawn position
    public Vector3 GenerateSpawnPos()
    {
        //variables
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        //assigning random variables to Vector variables
        Vector3 spawnPos = new Vector3(spawnPosX, 1.0f, spawnPosZ);

        return spawnPos;
    }

    public void ResetNextWave()
    {
        nextWave = 0;
    }

}
