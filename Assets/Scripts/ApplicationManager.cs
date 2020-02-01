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
    private float elapsedTime = 0;
    public float endOfProgress = 100;


    //The waterTiles in their Update need to call this method

    //TODO
    public void AddWater(float amount)
    {
        if (currentGameStatus != GameStatus.Playing) return;
        currentAmountOfWater += amount;
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
            Debug.Log("Stopping time...");
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("Starting time...");
            Time.timeScale = 1;
        }
    }

    public GameStatus currentGameStatus = GameStatus.InMenu;

    public void StartGame()
    {
        Debug.Log("Starting game...");
        if (!currentPlayer)
        {
            levelManager.PopulateTerrain();
            currentPlayer = Instantiate(playerPrefab, new Vector3(0, 6, 0), Quaternion.identity);
        }
        CloseMenu();

        //Camera.main.transform.parent=player.transform;

    }

    public void RestartGame() {
        Debug.Log("Restarting game...");
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
        elapsedTime += Time.deltaTime;
        inventoryMenu.transform.Find("Score").GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + (int)elapsedTime;
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
