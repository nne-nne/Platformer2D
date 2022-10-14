using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    GS_PAUSE_MENU,
    GS_GAME,
    GS_GAME_OVER,
    GS_LEVEL_COMPLETED
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState currentGameState;
    public Image[] keysTab;

    private int keys = 0;

    public int Keys { get => keys; }

    public void AddKey()
    {
        keysTab[keys].color = Color.yellow;
        keys++;

    }

    private void SetGameState(GameState newGameState)
    {
        Debug.Log("New game state: " + newGameState);
        currentGameState = newGameState;
    }

    private void InGame()
    {
        SetGameState(GameState.GS_GAME);
    } 

    private void GameOver()
    {
        SetGameState(GameState.GS_GAME_OVER);
    }

    private void PauseMenu()
    {
        SetGameState(GameState.GS_PAUSE_MENU);
    }

    private void LevelCompleted()
    {
        SetGameState(GameState.GS_LEVEL_COMPLETED);
    }


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        foreach(Image keyImg in keysTab)
        {
            keyImg.color = Color.grey;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu();
    }

    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGameState == GameState.GS_PAUSE_MENU)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                InGame();
            }
        }
    }
}
