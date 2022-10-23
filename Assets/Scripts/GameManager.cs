using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Image[] livesTab;
    public Image[] enemiesTab;
    public TMP_Text timerText;

    private int keys = 0;
    private int lives = 3;
    private int enemies = 2;

    private float timer = 0;

    public int Keys { get => keys; }
    public int Lives { get => lives; }

    public void AddKey()
    {
        keysTab[keys].color = Color.yellow;
        keys++;
    }

    public void AddLive()
    {
        livesTab[lives].enabled = true;
        lives++;
    }

    public void LoseLive()
    {
        lives--;
        livesTab[lives].enabled = false;
    }

    public void KillEnemy()
    {
        enemies--;
        enemiesTab[enemies].enabled = false;
    }

    public void SetGameState(GameState newGameState)
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

    public void GameOver()
    {
        SetGameState(GameState.GS_GAME_OVER);
    }

    private void PauseMenu()
    {
        SetGameState(GameState.GS_PAUSE_MENU);
    }

    public void LevelCompleted()
    {
        SetGameState(GameState.GS_LEVEL_COMPLETED);
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        foreach (Image keyImg in keysTab)
        {
            keyImg.color = Color.grey;
        }

        for (int i = 0; i < livesTab.Length; i++)
        {
            livesTab[i].enabled = lives > i;
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
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentGameState == GameState.GS_PAUSE_MENU)
            {
                InGame();
            }
            else
            {
                PauseMenu();
            }
        }

        Time.timeScale = currentGameState == GameState.GS_GAME ? 1f : 0;


        timer += Time.deltaTime;
        int timerSeconds = Mathf.FloorToInt(timer);
        timerText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(timerSeconds/60), timerSeconds%60);
    }
}
