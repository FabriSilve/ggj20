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

    public int spawnPointXSize;
    public int spawnPointZSize;


    Transform[] allLevelSpawnPoints;
    void InitializeScales()
    {
        spawnPointXSize = (int)(baseTerrain.transform.localScale.x / spawnPointPrefab.transform.localScale.x);
        spawnPointZSize = (int)(baseTerrain.transform.localScale.z / spawnPointPrefab.transform.localScale.z);

        allLevelSpawnPoints = new Transform[spawnPointXSize * spawnPointZSize];
        allSpawnPointLogic = new SpawnPoint[spawnPointXSize * spawnPointZSize];
    }


    public void PopulateTerrain()
    {
        //TODO I should destroy any existing ones

        InitializeScales();

        for (int i = 0; i < spawnPointZSize; i++)
        {
            for (int y = 0; y < spawnPointXSize; y++)
            {



                GameObject spawnPoint = Instantiate(spawnPointPrefab);
                //TODO fix the name it galls


                // spawnPoint.name = "SpawnPoint " + y + (i * (y / spawnPointXSize)).ToString();
                spawnPoint.name = ((i * spawnPointZSize) + y).ToString();

                allLevelSpawnPoints[i + y] = spawnPoint.transform;
                allSpawnPointLogic[i + y] = spawnPoint.GetComponent<SpawnPoint>();

                //It lowers Z 
                float x = -baseTerrain.transform.localScale.x / 2 + spawnPoint.transform.localScale.x / 2 + (y * spawnPoint.transform.localScale.x);
                float z = baseTerrain.transform.localScale.x / 2 - spawnPoint.transform.localScale.x / 2 - (i * spawnPoint.transform.localScale.x);

                spawnPoint.transform.position = new Vector3(x, 1, z);

            }
        }


    }







    // Update is called once per frame
    void Update()
    {
        //SpawnRandomWaterTile();
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
