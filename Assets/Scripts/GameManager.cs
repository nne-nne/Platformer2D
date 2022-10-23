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
    [SerializeField] Canvas levelCompletedCanvas;
    [SerializeField] Canvas gameOverCanvas;

    public GameState currentGameState;
    public Image[] keysTab;
    public Image[] livesTab;
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

        if (newGameState == GameState.GS_LEVEL_COMPLETED)
        {
            SetScoreText(levelCompletedScoreText);
            levelCompletedCanvas.enabled = true;
        }
        else
        {
            levelCompletedCanvas.enabled = false;
        }

        if (newGameState == GameState.GS_GAME_OVER)
        {
            SetScoreText(gameOverScoreText);
            gameOverCanvas.enabled = true;
        }
        else
        {
            gameOverCanvas.enabled = false;
        }

        Time.timeScale = currentGameState == GameState.GS_GAME ? 1f : 0;
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


        timer += Time.deltaTime;
        int timerSeconds = Mathf.FloorToInt(timer);
        timerText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(timerSeconds/60), timerSeconds%60);
    }
}
