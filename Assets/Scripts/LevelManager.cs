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

    public int weightNeighbors = 4;
    private int numberOfHoles;
    private int totalWeight;

    public GameObject holeSpawnerGameObject;
    private Spawnable holeSpawner;


    public GameObject tileSpawnerGameObject;
    private Spawnable tileSpawner;

    void Awake()
    {
        holeSpawner = holeSpawnerGameObject.GetComponent<Spawnable>();
        tileSpawner = tileSpawnerGameObject.GetComponent<Spawnable>();
    }

    Transform[,] allLevelSpawnPoints;

    void InitializeScales()
    {
        spawnPointXSize = (int)(baseTerrain.transform.localScale.x / spawnPointPrefab.transform.localScale.x);
        spawnPointZSize = (int)(baseTerrain.transform.localScale.z / spawnPointPrefab.transform.localScale.z);

        totalWeight = spawnPointXSize * spawnPointZSize;
        allLevelSpawnPoints = new Transform[spawnPointZSize, spawnPointXSize];
        grid = new SpawnPoint[spawnPointZSize, spawnPointXSize];
    }

    void Start()
    {
        InvokeRepeating("CheckSpawner", 5.0f, 4f);
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
                float x = -baseTerrain.transform.localScale.x / 2 + spawnPoint.transform.localScale.x / 2 + (j * spawnPoint.transform.localScale.x);
                float z = baseTerrain.transform.localScale.z / 2 - spawnPoint.transform.localScale.z / 2 - (i * spawnPoint.transform.localScale.z);

                spawnPoint.transform.position = new Vector3(x, 1, z);

                Transform transformTile = tileSpawner.Spawn(spawnPoint.transform);
                GameObject.Destroy(spawnPoint);
                allLevelSpawnPoints[i, j] = transformTile;

                SpawnPoint spawnPointComponent = transformTile.GetComponent<SpawnPoint>();

                grid[i, j] = spawnPointComponent;
                spawnPointComponent.column = i;
                spawnPointComponent.line = j;
            }
        }


    }




    SpawnPoint getNeighbor(int i, int j, Neighbors neighbor)
    {
        if (neighbor == Neighbors.left)
        {
            return j - 1 >= 0 ? grid[i, j - 1] : null;
        }
        if (neighbor == Neighbors.top)
        {
            return i - 1 >= 0 ? grid[i - 1, j] : null;
        }
        if (neighbor == Neighbors.right)
        {
            return j + 1 < spawnPointXSize ? grid[i, j + 1] : null;
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

    KeyValuePair<int, int> findCellWithWeights(int n)
    {
        int position = n;
        for (int i = 0; i < spawnPointZSize; i++)
        {
            for (int j = 0; j < spawnPointXSize; j++)
            {
                if (grid[i, j].state == State.empty)
                {
                    position = position - grid[i, j].weight;
                    if (position < 0)
                    {
                        return new KeyValuePair<int, int>(i, j);
                    }

                }
            }

        }
        return new KeyValuePair<int, int>(-1, -1);
    }

    void addWeightToAllNeighbors(int i, int j, int weight)
    {
        Neighbors[] listSides = new Neighbors[] { Neighbors.left, Neighbors.right, Neighbors.top, Neighbors.down };
        foreach (Neighbors side in listSides)
        {
            SpawnPoint neighbor = getNeighbor(i, j, side);
            if (neighbor != null)
            {
                if (neighbor.weight > 0 && neighbor.state == State.empty)
                {
                    neighbor.weight += weight;
                    totalWeight += weight;
                }
            }
        }
    }

    void equalizeWeights(int i, int j)
    {
        // We remove the own component weight, and reset the component weight to 1;
        totalWeight -= grid[i, j].weight;
        grid[i, j].weight = 1;
        addWeightToAllNeighbors(i, j, weightNeighbors);
    }

    public GameObject FixTile(GameObject brokenTile)
    {
        Debug.Log("tile fixed");
        SpawnPoint spawnPoint = brokenTile.GetComponent<SpawnPoint>();
        spawnPoint.weight = 1; // we reset the weight
        Transform oldTransform = allLevelSpawnPoints[spawnPoint.column, spawnPoint.line];
        Transform newTransform = tileSpawner.Spawn(oldTransform);
        holeSpawner.Hide(brokenTile);
        allLevelSpawnPoints[spawnPoint.column, spawnPoint.line] = newTransform;
        grid[spawnPoint.column, spawnPoint.line] = newTransform.GetComponent<SpawnPoint>();
        newTransform.GetComponent<SpawnPoint>().column = spawnPoint.column;
        newTransform.GetComponent<SpawnPoint>().line = spawnPoint.line;
        return newTransform.gameObject;
    }

    void CheckSpawner()
    {
        int randompoint = Random.Range(0, this.totalWeight);
        KeyValuePair<int, int> pairPosition = findCellWithWeights(randompoint);
        if (pairPosition.Key < 0 || pairPosition.Value < 0)
        {
            return;
        }
        equalizeWeights(pairPosition.Key, pairPosition.Value);
        Transform oldTransform = allLevelSpawnPoints[pairPosition.Key, pairPosition.Value];


        Transform newTransform = holeSpawner.Spawn(oldTransform);
        Debug.Log(tileSpawner.Hide(oldTransform.gameObject));
        allLevelSpawnPoints[pairPosition.Key, pairPosition.Value] = newTransform;
        grid[pairPosition.Key, pairPosition.Value] = newTransform.GetComponent<SpawnPoint>();

        //Those complain
        newTransform.GetComponent<SpawnPoint>().column = pairPosition.Key;
        newTransform.GetComponent<SpawnPoint>().line = pairPosition.Value;
    }
}
