using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private float scoreMultiplayer = 1f;
    private float score;

    public const string HighScoreKey = "HighScore";
    public const string CurrentScoreKey = "CurrentScore";
    private const string CurrentDifficulty = "CurrentDifficulty";

    private float diffIncrease=0.5f;
    private float diffLevel;
    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        score = PlayerPrefs.GetInt(CurrentScoreKey, 0);
        updateScore();
        diffLevel = PlayerPrefs.GetFloat(CurrentDifficulty);
    }
 

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Car>().getStopped())
        {
        
            return;
        }
        score += Time.deltaTime*scoreMultiplayer + diffLevel*diffIncrease;
        updateScore();
    }

    public void updateScore()
    {
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public void setCurScore()
    {
        PlayerPrefs.SetInt(CurrentScoreKey, Mathf.FloorToInt(score));
    }


    private void OnDestroy()
    {
      
        
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if(score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(score));
        }
    }
  
}
