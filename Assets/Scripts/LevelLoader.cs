using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public const string HighScoreKey = "HighScore";
    public const string CurrentScoreKey = "CurrentScore";
    public const string CurrentPlayedLevel= "CurrentLevel";
    private int usedAd = 0;

    public void loadGameplayScene()
    {
        if (FindObjectOfType<GameController>().canPlay())
        {
           PlayerPrefs.SetInt(CurrentScoreKey,0);
            SceneManager.LoadScene("Scene_Gameplay");
        }
       
    }
 
    public void loadMainMenu()
    {
        PlayerPrefs.SetInt(CurrentScoreKey,0);
        SceneManager.LoadScene("Scene_MainMenu");
    }
    public void loadSettingsMenu()
    {
        SceneManager.LoadScene("Scene_Settings");
    }
    public void loadLevelSelectionMenu()
    {
        SceneManager.LoadScene("Scene_LevelSelection");
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
    public void nextLevel()
    {

        FindObjectOfType<Canvas>().transform.Find("GameplayScoreText").gameObject.SetActive(true);
        FindObjectOfType<Canvas>().transform.Find("WinPanel").gameObject.SetActive(false);


        int curPlayed = PlayerPrefs.GetInt(CurrentPlayedLevel,0);
        
        GameObject nextSpawnPoint = GameObject.Find("SpawnPoints").transform.GetChild(curPlayed+1).gameObject;
        GameObject car = FindObjectOfType<Car>().gameObject;
        //PlayerPrefs.SetInt(CurrentPlayedLevel, curPlayed+1);



        switch (nextSpawnPoint.GetComponent<Identity>().getRotation())
        {
            case 1:
                car.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
                car.GetComponent<Rigidbody>().rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            case 2:   
                car.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                car.GetComponent<Rigidbody>().rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                break;
            case 3:
                car.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                car.GetComponent<Rigidbody>().rotation = Quaternion.Euler(new Vector3(0,180, 0));
                break;
            case 4:
                car.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
                car.GetComponent<Rigidbody>().rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                break;
        }
        car.transform.position = nextSpawnPoint.transform.position;
        car.GetComponent<Car>().setStopped(false);

        PlayerPrefs.SetInt(CurrentPlayedLevel, curPlayed + 1);


    }

    //Advertisement
    public void Continue()
    {
        if (usedAd < 3)
        {
            FindObjectOfType<AdManager>().ShowAd();
            usedAd++;
        }

    }
}
