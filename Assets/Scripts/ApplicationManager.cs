using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
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
        //TODO Hide the Menu,
        //Spawn the Player,
        //Let the LevelManager Know what to do.
        mainMenu.SetActive(currentGameStatus == GameStatus.InMenu);
        Time.timeScale = 1;

        currentGameStatus = GameStatus.Playing;
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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
