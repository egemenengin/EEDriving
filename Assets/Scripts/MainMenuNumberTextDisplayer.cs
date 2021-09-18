using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuNumberTextDisplayer : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (transform.tag == "HighScore")
        {
            text.text = PlayerPrefs.GetInt(ScoreDisplay.HighScoreKey, 0).ToString();
        }
        if (transform.tag == "Fuel")
        {
            text.text = PlayerPrefs.GetInt(GameController.EnergyKey, 3).ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.tag == "Fuel")
        {
            text.text = PlayerPrefs.GetInt(GameController.EnergyKey, 3).ToString();
        }
    }
  
}
