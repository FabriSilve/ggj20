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

                allLevelSpawnPoints[(i * spawnPointZSize) + j] = spawnPoint.transform;
                SpawnPoint spawnPointComponent = spawnPoint.GetComponent<SpawnPoint>();

                grid[i,j] = spawnPointComponent;

                //It lowers Z 
                float x = -baseTerrain.transform.localScale.x / 2 + spawnPoint.transform.localScale.x / 2 + (j * spawnPoint.transform.localScale.x);
                float z = baseTerrain.transform.localScale.x / 2 - spawnPoint.transform.localScale.x / 2 - (i * spawnPoint.transform.localScale.x);

                spawnPoint.transform.position = new Vector3(x, 1, z);

            }
        }


    }




    SpawnPoint getNeighbor(int i, int j, Neighbors neighbor)
    {
            if (neighbor == Neighbors.left)
            {
                return j - 1 >= 0 ? grid[i, j-1] : null;
            }
            if (neighbor == Neighbors.top){
                return i - 1 >= 0 ? grid[i-1, j] : null;
            }
            if (neighbor == Neighbors.right)
            {
                return j + 1 < spawnPointXSize ? grid[i, j+1] : null;
            }
            if (neighbor == Neighbors.down)
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

    int findCellWithWeights(int n)
    {
        int position = n;
        for (int i = 0; i < spawnPointZSize; i++)
        {
            for (int j = 0; j < spawnPointXSize; j++)
            {
                //Debug.Log(grid[i, j].weight);
                position = position - grid[i, j].weight;
                if (position < 0)
                {
                    grid[i, j].weight = 0;
                    totalWeight -= 1;
                    addWeightToAllNeighbors(i, j, 400);
                    //Debug.Log(i + j);
                    return (i * spawnPointZSize) + j;
                }
            }

        }
        return -1;
    }
    void addWeightToAllNeighbors(int i, int j, int weight)
    {
        Neighbors[] listSides = new Neighbors[] { Neighbors.left, Neighbors.right, Neighbors.top, Neighbors.down };
        foreach (Neighbors side in listSides) {
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
        int itemPosition = findCellWithWeights(randompoint);
        if (itemPosition < 0)
        {
            return;
        }
        Transform oldTransform = allLevelSpawnPoints[itemPosition];
        Transform newTransform = holeSpawner.Spawn(oldTransform);
        GameObject.Destroy(oldTransform.gameObject);
        allLevelSpawnPoints[itemPosition] = newTransform;
    }
}
