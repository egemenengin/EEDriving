using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private float scoreMultiplayer = 1f;
    private float score = 0;

    public const string HighScoreKey = "HighScore";
    // Start is called before the first frame update
    void Start()
    {
        scoreText = FindObjectOfType<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime*scoreMultiplayer;
        updateScore();
    }

    public void updateScore()
    {
        scoreText.text = Mathf.FloorToInt(score).ToString();
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
