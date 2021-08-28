using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    
    public void loadGameplayScene()
    {
        if (FindObjectOfType<GameController>().canPlay())
        {
            SceneManager.LoadScene(2);
        }
       
    }
    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void loadSettingsMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void quitGame()
    {
        Application.Quit();
    }

    public void resetHighScore()
    {
        PlayerPrefs.SetInt(ScoreDisplay.HighScoreKey, 0);
    }
}
