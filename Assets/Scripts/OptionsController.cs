using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;
    const float MIN_DIFFICULTY = 0f;
    const float MAX_DIFFICULTY = 4f;


    private const string CurrentVolume = "CurrentVolume";
    private const string CurrentDifficulty = "CurrentDifficulty";


    Slider volumeSlider;
    Slider difficultySlider;

    float defaultVolume = 0.2f;
    float defaultDifficulty = 0;

    private void Awake()
    {
        volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        difficultySlider = GameObject.Find("DifficultySlider").GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(CurrentVolume, defaultVolume);
        difficultySlider.value = PlayerPrefs.GetFloat(CurrentDifficulty, defaultDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setDefault()
    {
        volumeSlider.value = defaultVolume;
        difficultySlider.value = defaultDifficulty;
        PlayerPrefs.SetFloat(CurrentVolume,defaultVolume);
        PlayerPrefs.SetFloat(CurrentDifficulty, defaultDifficulty);
        
    }
    public void saveAndExit()
    {
        setVolume(volumeSlider.value);
        setDifficulty(difficultySlider.value);
        SceneManager.LoadScene("Scene_MainMenu");
    }

    public void setVolume(float newVol)
    {
        if (newVol >= MIN_VOLUME && newVol <= MAX_VOLUME)
        {
            PlayerPrefs.SetFloat(CurrentVolume, volumeSlider.value);
        }
        else
        {
            Debug.LogError("DIFFCULTY IS OUT OF RANGE");
        }
      
    }
    public void setDifficulty(float newDiff)
    {
        if (newDiff >= MIN_DIFFICULTY && newDiff <= MAX_DIFFICULTY)
        {
            PlayerPrefs.SetFloat(CurrentDifficulty, difficultySlider.value);
        }
        else
        {
            Debug.LogError("DIFFCULTY IS OUT OF RANGE");
        }


        
    }




    #region ResetHighScore
    public void resetHighScore()
    {
        PlayerPrefs.SetInt(ScoreDisplay.HighScoreKey, 0);
    }
    #endregion

}
