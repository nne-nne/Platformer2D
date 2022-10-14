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

    [SerializeField] Canvas inGameCanvas;

    public GameState currentGameState;
    public Image[] keysTab;

    private void SetGameState(GameState newGameState)
    {
        Debug.Log("New game state: " + newGameState);
        currentGameState = newGameState;

        if(newGameState == GameState.GS_GAME)
        {
            inGameCanvas.enabled = true;
        }
        else
        {
            inGameCanvas.enabled = false;
        }
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
