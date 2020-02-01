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
    SpawnPoint[,] grid;

    public int spawnPointXSize;
    public int spawnPointZSize;

    public int numberOfHoles;
    public int totalWeight;

    public GameObject holeSpawnerGameObject;
    private Spawnable holeSpawner;

    void Awake() {
        holeSpawner = holeSpawnerGameObject.GetComponent<Spawnable>();
    }

    Transform[] allLevelSpawnPoints;
    void InitializeScales()
    {
        spawnPointXSize = (int)(baseTerrain.transform.localScale.x / spawnPointPrefab.transform.localScale.x);
        spawnPointZSize = (int)(baseTerrain.transform.localScale.z / spawnPointPrefab.transform.localScale.z);
        totalWeight = spawnPointXSize * spawnPointZSize;
        allLevelSpawnPoints = new Transform[spawnPointXSize * spawnPointZSize];
        grid = new SpawnPoint[spawnPointXSize, spawnPointZSize];
    }

    void Start()
    {
        InvokeRepeating("CheckSpawner", 2.0f, 2f);
    }
    public void PopulateTerrain()
    {
        //TODO I should destroy any existing ones

        InitializeScales();

        for (int i = 0; i < spawnPointZSize; i++)
        {
            for (int j = 0; j < spawnPointXSize; j++)
            {


                GameObject spawnPoint = Instantiate(spawnPointPrefab);
                //TODO fix the name it galls


                // spawnPoint.name = "SpawnPoint " + j + (i * (j / spawnPointXSize)).ToString();
                spawnPoint.name = ((i * spawnPointZSize) + j).ToString();

                allLevelSpawnPoints[i + j] = spawnPoint.transform;
                SpawnPoint spawnPointComponent = spawnPoint.GetComponent<SpawnPoint>();

                grid[i,j] = spawnPointComponent;

                //It lowers Z 
                float x = -baseTerrain.transform.localScale.x / 2 + spawnPoint.transform.localScale.x / 2 + (j * spawnPoint.transform.localScale.x);
                float z = baseTerrain.transform.localScale.x / 2 - spawnPoint.transform.localScale.x / 2 - (i * spawnPoint.transform.localScale.x);

                spawnPoint.transform.position = new Vector3(x, 1, z);

            }
        }


    }




    SpawnPoint getNeighbor(int i, int j, CellConnections direction)
    {
            if (direction == CellConnections.left)
            {
                return j - 1 > 0 ? grid[i, j-1] : null;
            }
            if (direction == CellConnections.top){
                return i - 1 > 0 ? grid[i-1, j] : null;
            }
            if (direction == CellConnections.right)
            {
                return j + 1 < spawnPointXSize ? grid[i, j+1] : null;
            }
            if (direction == CellConnections.down)
            {
                return i + 1 < spawnPointZSize ? grid[i + 1, j] : null;
            }
        return null;

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

    Transform findCellWithWeights(int n)
    {
        int position = n;
        for (int i = 0; i < spawnPointZSize; i++)
        {
            for (int j = 0; j < spawnPointXSize; j++)
            {
                position = position - grid[i, j].weight;
                if (position < 0)
                {
                    addWeightToAllNeighbors(i, j, 4);
                    return allLevelSpawnPoints[i + j];
                }
            }

        }
        return null;
    }
    void addWeightToAllNeighbors(int i, int j, int weight)
    {
        CellConnections[] listSides = new CellConnections[] { CellConnections.left, CellConnections.right, CellConnections.top, CellConnections.down };
        foreach (CellConnections side in listSides) {
            SpawnPoint neighbor = getNeighbor(i, j, side);
            if (neighbor != null) {
                if (neighbor.weight > 0)
                {
                    neighbor.weight += weight;
                    totalWeight += weight;
                }
            }
        }
    }

    void CheckSpawner()
    {
        int randompoint = Random.Range(0, this.totalWeight);
        Transform itemPosition = findCellWithWeights(randompoint);
        holeSpawner.Spawn(itemPosition);
    }
}
