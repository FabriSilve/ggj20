using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
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


    void UpdateMenus()
    {
        mainMenu.SetActive(currentGameStatus == GameStatus.InMenu);
        shopMenu.SetActive(currentGameStatus == GameStatus.Shopping);

        if (currentGameStatus == GameStatus.InMenu)
        {
            Time.timeScale = 0;
        }
        else
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }
    }

    public GameStatus currentGameStatus = GameStatus.InMenu;

    public void StartGame()
    {
        if (!currentPlayer)
        {
            levelManager.PopulateTerrain();
            currentPlayer = Instantiate(playerPrefab, new Vector3(0, 6, 0), Quaternion.identity);
        }
        CloseMenu();

        //Camera.main.transform.parent=player.transform;

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
        currentGameStatus = GameStatus.Playing;
        UpdateMenus();

    }

    private void Update()
    {
        ToggleMenu();
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
