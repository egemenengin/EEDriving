using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelection : MonoBehaviour
{

    public const string UnlockedLevel = "LastUnlockedLevel";
    public const string CurrentPlayedLevel = "CurrentLevel";
    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt(UnlockedLevel, 0);
        
        for(int i = 1; i< unlockedLevel; i++)
        {
            Debug.Log(FindObjectOfType<Canvas>().transform.GetChild(i + 3).name);
            FindObjectOfType<Canvas>().transform.GetChild(i+3).gameObject.GetComponent<Button>().interactable = true;
            FindObjectOfType<Canvas>().transform.GetChild(i + 3).Find("Image").gameObject.SetActive(false);
            FindObjectOfType<Canvas>().transform.GetChild(i + 3).Find("Text").gameObject.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void selectLevel(int level)
    {
        PlayerPrefs.SetInt(CurrentPlayedLevel,level);
    }
}
