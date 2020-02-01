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
        currentGameStatus = GameStatus.Playing;

        //TODO Hide the Menu,
        //Spawn the Player,
        //Let the LevelManager Know what to do.
        mainMenu.SetActive(currentGameStatus == GameStatus.InMenu);
        Time.timeScale = 1;

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

}
