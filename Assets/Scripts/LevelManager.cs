using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //TODO Make the Application Manager use the methods here. 

    [SerializeField]
    GameObject baseTerrain;

    [SerializeField]
    GameObject spawnPointPrefab;

    [SerializeField]
    SpawnPoint[] allSpawnPointLogic;

    int spawnPointXSize;
    int spawnPointZSize;


    Transform[] allLevelSpawnPoints;
    void InitializeScales()
    {
        spawnPointXSize = (int)(baseTerrain.transform.localScale.x / spawnPointPrefab.transform.localScale.x);
        spawnPointZSize = (int)(baseTerrain.transform.localScale.z / spawnPointPrefab.transform.localScale.z);

        allLevelSpawnPoints = new Transform[spawnPointXSize * spawnPointZSize];
    }


    public void PopulateTerrain()
    {
        //TODO I should destroy any existing ones

        InitializeScales();

        for (int i = 0; i < spawnPointXSize; i++)
        {
            for (int y = 0; y < spawnPointZSize; y++)
            {

                GameObject spawnPoint = Instantiate(spawnPointPrefab);
                spawnPoint.name = "SpawnPoint " + i + y;
                allLevelSpawnPoints[i + y] = spawnPoint.transform;
                allSpawnPointLogic[i] = spawnPoint.GetComponent<SpawnPoint>();

                //I feel I have to do something more here
                float x = -baseTerrain.transform.localScale.x / 2 + spawnPoint.transform.localScale.x / 2 + (i * spawnPoint.transform.localScale.x);
                float z = baseTerrain.transform.localScale.x / 2 - spawnPoint.transform.localScale.x / 2 - (y * spawnPoint.transform.localScale.x);

                spawnPoint.transform.position = new Vector3(x, 1, z);

            }
        }


    }


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



    void SpawnRandomWaterTile()
    {
        int randompoint = Random.Range(0, allLevelSpawnPoints.Length);
        if (!allSpawnPointLogic[randompoint].isOccupied)
        {
            //Spawn a Water Tile
            allSpawnPointLogic[randompoint].isOccupied = true;
        }



    }
}
