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
            SceneManager.LoadScene("Scene_Gameplay");
        }
       
    }
    public void loadMainMenu()
    {
        SceneManager.LoadScene("Scene_MainMenu");
    }
    public void loadSettingsMenu()
    {
        SceneManager.LoadScene("Scene_Settings");
    }

    public void loadMarket()
    {
        SceneManager.LoadScene("Scene_Market");
    }
    public void loadInventory()
    {
        SceneManager.LoadScene("Scene_Inventory");
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
