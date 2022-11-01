using System.Collections;
using System.Collections.Generic;
using System;
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
    [SerializeField] Canvas levelCompletedCanvas;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas pauseMenuCanvas;

    public GameState currentGameState;
    public Image[] keysTab;
    public Image[] livesTab;
    [SerializeField] bool showEnemiesLeft = true;
    public Image[] enemiesTab;
    public TMP_Text timerText;
    public TMP_Text gameOverScoreText;
    public TMP_Text levelCompletedScoreText;

    private int keys = 0;
    private int lives = 3;
    private int maxLives = 4;
    private int enemies = 2;
    private int maxEnemies = 2;
    private bool keysCompleted = false;

    [SerializeField] int maxKeyNumber = 3;
    

    private float timer = 0;

    public int Keys { get => keys; }
    public int Lives { get => lives; }
    public int MaxKeyNumber { get => maxKeyNumber; }
    public bool KeysCompleted { get => keysCompleted; }

    public void AddKey()
    {
        keysTab[keys].color = Color.yellow;
        keys++;
        if(keys >= maxKeyNumber)
        {
            keysCompleted = true;
        }
    }

    public void AddLive()
    {
        if(livesTab.Length == lives)
        {
            Debug.Log("Cannot exceed the maximum number of lives");
            return;
        }

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
        if (!showEnemiesLeft)
            return;

        enemies--;
        enemiesTab[enemies].enabled = false;
    }

    private double CountScore()
    {
        double timeBonus = 60.0 - timer;
        if (timeBonus < 0) timeBonus = 0;
        return timeBonus + (maxEnemies - enemies) * 20.0 + (maxLives -  lives )*10.0;
    }

    private void SetScoreText(TMP_Text textField)
    {
        double score = CountScore();
        string scoreTextValue = $"Score: {score.ToString("F1")}";
        textField.text = scoreTextValue;
    }

    public void InGame()
    {
        SetGameState(GameState.GS_GAME);
    }

    public void GameOver()
    {
        SetScoreText(gameOverScoreText);
        SetGameState(GameState.GS_GAME_OVER);
    }

    public void PauseMenu()
    {
        SetGameState(GameState.GS_PAUSE_MENU);
    }

    public void LevelCompleted()
    {
        SetScoreText(levelCompletedScoreText);
        SetGameState(GameState.GS_LEVEL_COMPLETED);
    }


    public void SetGameState(GameState newGameState)
    {
        Debug.Log("New game state: " + newGameState);
        currentGameState = newGameState;

        inGameCanvas.enabled = (currentGameState == GameState.GS_GAME);
        pauseMenuCanvas.enabled = (currentGameState == GameState.GS_PAUSE_MENU);
        gameOverCanvas.enabled = (currentGameState == GameState.GS_GAME_OVER);
        levelCompletedCanvas.enabled = (currentGameState == GameState.GS_LEVEL_COMPLETED);

        Time.timeScale = currentGameState == GameState.GS_GAME ? 1f : 0;
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentGameState == GameState.GS_GAME)
            {
                PauseMenu();
            }
            else if (currentGameState == GameState.GS_PAUSE_MENU)
            {
                InGame();
            }
        }
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

        if (!showEnemiesLeft)
        {
            // Disable enemy count display
            foreach(var enemyDisplay in enemiesTab)
            {
                enemyDisplay.enabled = false;
            }
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InGame();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();


        timer += Time.deltaTime;
        int timerSeconds = Mathf.FloorToInt(timer);
        timerText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(timerSeconds/60), timerSeconds%60);
    }
}
