using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //variables

    public GameObject player;
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    private float spawnRange = 7;
    public int enemyCount;
    public int nextWave;
    private float playerSafetyZone = 2f;

    public bool gameActive = false;




    // Start is called before the first frame update
    void Start()
    {

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
            Instantiate(enemyPrefab, GenerateSpawnPos(0), enemyPrefab.transform.rotation);
        }
    }


    //method to spawn power up
    public void SpawnPowerUp()
    {
        Instantiate(powerUpPrefab, GenerateSpawnPos(1.0f), enemyPrefab.transform.rotation);
    }


    //method to generate a new random spawn position
    //float parameter helps differs between enemy & power-up spawn height on y Axis
    public Vector3 GenerateSpawnPos(float objectYPosition)
    {
        //variables
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(spawnPosX, objectYPosition, spawnPosZ);

        //ensures spawn position isn't too close to player position
        while (Vector3.Distance(spawnPos, player.transform.position) < playerSafetyZone)
        {
            spawnPosX = Random.Range(-spawnRange, spawnRange);
            spawnPosZ = Random.Range(-spawnRange, spawnRange);
            spawnPos = new Vector3(spawnPosX, objectYPosition, spawnPosZ);
            //Debug.Log(Vector3.Distance(spawnPos, player.transform.position));
        }

        return spawnPos;

    }



    public void ResetNextWave()
    {
        nextWave = 0;
    }

}
