using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public TMP_Text highScoreLevel1Text;
    public TMP_Text highScoreLevel2Text;
    public IEnumerator StartGame(string levelName)
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene(levelName);
    }
    public void onLevel1ButtonPressed()
    {
        StartCoroutine(StartGame("Level1"));
    }

    public void onLevel2ButtonPressed()
    {
        StartCoroutine(StartGame("Level2"));
    }

    public void onLevel3ButtonPressed()
    {
        StartCoroutine(StartGame("Level3"));
    }

    public void onExitButtonPressed()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("HighscoreLevel1"))
        {
            PlayerPrefs.SetInt("HighscoreLevel1", 0);
        }

        if (!PlayerPrefs.HasKey("HighscoreLevel2"))
        {
            PlayerPrefs.SetInt("HighscoreLevel2", 0);
        }
        if (highScoreLevel1Text != null)
            highScoreLevel1Text.text = $"High Score: {PlayerPrefs.GetInt("HighscoreLevel1")}";

        if (highScoreLevel2Text != null)
            highScoreLevel2Text.text = $"High Score: {PlayerPrefs.GetInt("HighscoreLevel2")}";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
