using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int maxEnergy = 3;
    private int energyRechargeDuration = 1;

    private int energy;

    public const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";

    public void Awake()
    {
        setUpSingleton();
        OnApplicationFocus(true);
    }
    public void Start()
    {
        OnApplicationFocus(true);
    }
    public void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) { return; }
        CancelInvoke();
        energy = PlayerPrefs.GetInt(EnergyKey,maxEnergy);
        if(energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey,string.Empty);
            if (energyReadyString == string.Empty)
            {
                
                return;
            }

            DateTime energyReady = DateTime.Parse(energyReadyString);

            if (DateTime.Now > energyReady)
            {
                int increseAmount = (DateTime.Now - energyReady).Minutes / 1;
                energy += increseAmount;
                if(energy > 3)
                {
                    energy = 3;
                }
                PlayerPrefs.SetInt(EnergyKey,energy);
            }
            
        }
    }
    private void Update()
    {
        string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
        if (energyReadyString.Equals(string.Empty))
        {
            return;
        }
        DateTime energyReady = DateTime.Parse(energyReadyString);
        if (DateTime.Now > energyReady)
        {
            increaseEnergy();
            energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString());
        }
    }

    private void setUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);          
        }
    }


    private void increaseEnergy()
    {
        if (energy < 3)
        {
            PlayerPrefs.SetInt(EnergyKey, ++energy);
        }
       
    }
    public bool canPlay()
    {
        if (energy < 1)
        {
            return false;
        }
        else
        {
            PlayerPrefs.SetInt(EnergyKey,--energy);
            int usedEnergy = maxEnergy - energy;
            DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(EnergyReadyKey,energyReady.ToString());
#if UNITY_ANDROID
            GetComponent<AndroidNotificationHandler>().ScheduleNotification(energyReady.AddMinutes((energyRechargeDuration*usedEnergy)));
#endif
#if UNITY_IOS
            GetComponent<IOSNotificationHandler>().ScheduleNotification(energyRechargeDuration);
#endif
            return true;
        }
    }


   
}
