using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    int timeDelay = 15;
    Transform[] allPoints;
    bool foundSpawnPoint = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnRandomWaterTile();
    }

    void SaveData()
    {
        int xpAmmount = 100;
        PlayerPrefs.SetInt("Score", xpAmmount);
    }

    void CalculateTimeInterval()
    {

    }

    void SpawnRandomWaterTile()
    {
        while (!foundSpawnPoint)
        {

        }

    }
}
