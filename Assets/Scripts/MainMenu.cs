using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public IEnumerator StartGame(string levelName)
    {
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

    // Update is called once per frame
    void Update()
    {

    }
}
