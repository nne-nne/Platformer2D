using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameState
{
    GS_PAUSE_MENU,
    GS_GAME,
    GS_GAME_OVER,
    GS_LEVEL_COMPLETED,
    GS_OPTIONS
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Canvas inGameCanvas;
    [SerializeField] Canvas levelCompletedCanvas;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas pauseMenuCanvas;
    [SerializeField] Canvas optionsCanvas;

    public GameState currentGameState;
    public Image[] keysTab;
    public Image[] livesTab;
    [SerializeField] bool showEnemiesLeft = true;
    public Image[] enemiesTab;
    public TMP_Text timerText;
    public TMP_Text gameOverScoreText;
    public TMP_Text levelCompletedScoreText;
    public TMP_Text levelCompletedHighScoreText;
    public TMP_Text gameOverHighScoreText;

    private int keys = 0;
    private int lives = 3;
    private int maxLives = 4;
    private int enemies = 2;
    private int maxEnemies = 2;
    private bool keysCompleted = false;
    private int maxSecsToHighScore = 80;

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

        if (lives == 0)
            GameOver();
    }

    public void KillEnemy()
    {
        if (!showEnemiesLeft)
            return;

        enemies--;
        enemiesTab[enemies].enabled = false;
    }

    private int CountScore()
    {
        double timeBonus = maxSecsToHighScore - timer;
        if (timeBonus < 0) timeBonus = 0;
        return (int)(timeBonus + (maxEnemies - enemies) * 20.0 + (maxLives -  lives )*10.0);
    }

    private void SetScoreText(TMP_Text textField)
    {
        int score = CountScore();
        string scoreTextValue = $"Score: {score}";
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

    public void Options()
    {
        SetGameState(GameState.GS_OPTIONS);
    }

    private IEnumerator ChangeTimePace(float target, int frames)
    {
        float originalTimeScale = Time.timeScale;
        int frame = 0;
        while(frame < frames)
        {
            Time.timeScale = Mathf.Lerp(originalTimeScale, target, frame/frames);
            frame++;
            yield return null;
        }
        Time.timeScale = target;
    }


    public void SetGameState(GameState newGameState)
    {
        Debug.Log("New game state: " + newGameState);
        currentGameState = newGameState;

        inGameCanvas.enabled = (currentGameState == GameState.GS_GAME);
        pauseMenuCanvas.enabled = (currentGameState == GameState.GS_PAUSE_MENU);
        gameOverCanvas.enabled = (currentGameState == GameState.GS_GAME_OVER);
        levelCompletedCanvas.enabled = (currentGameState == GameState.GS_LEVEL_COMPLETED);
        optionsCanvas.enabled = (currentGameState == GameState.GS_OPTIONS);

        if (newGameState == GameState.GS_LEVEL_COMPLETED || currentGameState == GameState.GS_GAME_OVER)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Level1")
            {
                int score = CountScore();
                if (score > PlayerPrefs.GetInt("HighscoreLevel1"))
                {
                    PlayerPrefs.SetInt("HighscoreLevel1", score);
                }
                levelCompletedHighScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighscoreLevel1")}";
                gameOverHighScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighscoreLevel1")}";
                levelCompletedScoreText.text = $"Score: {score}";
                gameOverScoreText.text = $"Score: {score}";
            }
            if (currentScene.name == "Level2")
            {
                int score = CountScore();
                if (score > PlayerPrefs.GetInt("HighscoreLevel2"))
                {
                    PlayerPrefs.SetInt("HighscoreLevel2", score);
                }
                levelCompletedHighScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighscoreLevel2")}";
                gameOverHighScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighscoreLevel2")}";
                levelCompletedScoreText.text = $"Score: {score}";
                gameOverScoreText.text = $"Score: {score}";
            }
        }
        
        float targetTimeScale = currentGameState == GameState.GS_GAME ? 1f : 0;
        StopAllCoroutines();
        StartCoroutine(ChangeTimePace(targetTimeScale, 50));
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

        if (!PlayerPrefs.HasKey("HighscoreLevel1"))
        {
            PlayerPrefs.SetInt("HighscoreLevel1", 0);
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

    private bool ShouldGenerateExitPlatform
        => LevelGenerator.Instance != null &&
        LevelGenerator.Instance.ShouldFinish == false &&
        timer >= LevelGenerator.Instance.MaxGameTime;

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

        timer += Time.deltaTime;
        int timerSeconds = Mathf.FloorToInt(timer);
        timerText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(timerSeconds/60), timerSeconds%60);

        if (ShouldGenerateExitPlatform)
        {
            LevelGenerator.Instance.GenerateExit();
        }
    }
}
