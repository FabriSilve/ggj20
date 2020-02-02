using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{

    public float maxAmountOfWaterAllowed = 100;
    [SerializeField]
    private float currentAmountOfWater = 0;
    [SerializeField]
    private float progress = 0;
    public float endOfProgress = 100;

    public GameObject water;


    //The waterTiles in their Update need to call this method

    //TODO
    public void AddWater(float amount)
    {
        if (currentGameStatus != GameStatus.Playing) return;
        currentAmountOfWater += amount;
        water.GetComponent<WaterLevelManager>().IncreaseWaterLevel();
        if (currentAmountOfWater > maxAmountOfWaterAllowed)
        {
            Debug.Log("You lost!");
            OpenGameOverMenu();
        }
    }

    [SerializeField]
    LevelManager levelManager;



    private static ApplicationManager _instance;
    public static ApplicationManager Instance { get { return _instance; } }

    [SerializeField]
    GameObject playerPrefab;

    GameObject currentPlayer;

    //I need a player ref

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject shopMenu;
    [SerializeField]
    private GameObject inventoryMenu;
    [SerializeField]
    private GameObject gameOverMenu;

    public enum GameStatus
    {
        InMenu,
        Playing,
        Shopping,
        GameOver
    }

    public void OpenShopingMenu()
    {
        currentGameStatus = GameStatus.Shopping;
        UpdateMenus();
    }


    public void CloseShopingMenu()
    {
        currentGameStatus = GameStatus.Playing;
        UpdateMenus();
    }

    public void OpenGameOverMenu() {
        currentGameStatus = GameStatus.GameOver;
        UpdateMenus();
    }


    void UpdateMenus()
    {
        mainMenu.SetActive(currentGameStatus == GameStatus.InMenu);
        shopMenu.SetActive(currentGameStatus == GameStatus.Shopping);
        gameOverMenu.SetActive(currentGameStatus == GameStatus.GameOver);

        if (currentGameStatus == GameStatus.InMenu || currentGameStatus == GameStatus.GameOver)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public GameStatus currentGameStatus = GameStatus.InMenu;

    public float GetProgress() {
        return (float)progress/(float)endOfProgress;
    }

    public void StartGame()
    {
        if (!currentPlayer)
        {
            levelManager.PopulateTerrain();
            currentPlayer = Instantiate(playerPrefab, new Vector3(0, 6, 0), Quaternion.identity);
            currentPlayer.GetComponent<PlayerBehavior>().levelManager = levelManager;
        }
        CloseMenu();

        //Camera.main.transform.parent=player.transform;

    }

    public void RestartGame() {
        SceneManager.LoadScene("Level1");
    }

    public void PauseGame()
    {
        currentGameStatus = GameStatus.InMenu;
        UpdateMenus();
    }

    public void EndGame()
    {
        // IF we have time ADD Score
    }
    // Start is called before the first frame update

    void CloseMenu()
    {
        Debug.Log("Closing menus...");
        currentGameStatus = GameStatus.Playing;
        UpdateMenus();

    }

    private void Update()
    {
        UpdateScore();
        ToggleMenu();
    }

    //TODO: Score should not just be a function of the elapsed time.
    public void UpdateScore() {
        if (currentGameStatus == GameStatus.Playing) {
            progress += Time.deltaTime;
            inventoryMenu.transform.Find("Score").GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + (int)progress;
        }
    }

    public void ToggleMenu()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Executed");
            if (currentGameStatus == GameStatus.InMenu)
            {
                CloseMenu();
            }
            else if (currentGameStatus == GameStatus.Playing)
            {
                PauseGame();
            }

        }
    }
}
