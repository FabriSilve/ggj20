using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField]
    LevelManager levelManager;
    private static ApplicationManager _instance;
    public static ApplicationManager Instance;


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

    public enum GameStatus
    {
        InMenu,
        Playing,
        GameOver
    }
    public GameStatus currentGameStatus = GameStatus.InMenu;

    public void StartGame()
    {
        CloseMenu();
        levelManager.PopulateTerrain();
    }

    public void PauseGame()
    {
        mainMenu.SetActive(currentGameStatus == GameStatus.InMenu);
        Time.timeScale = 0;
    }

    public void EndGame()
    {
        // IF we have time ADD Score
    }
    // Start is called before the first frame update

    void CloseMenu()
    {
        currentGameStatus = GameStatus.Playing;
        Time.timeScale = 1;
        mainMenu.SetActive(currentGameStatus == GameStatus.InMenu);

    }

    private void Update()
    {
        ToggleMenu();
    }

    public void ToggleMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
