using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_ANDROID
    private string gameId = "4372285";
#elif UNITY_IOS
    private string gameId = "4372284"; 
#endif
    [SerializeField] bool testMode = true;
    private void Awake()
    {
        setUpSingleton();
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
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId, testMode);
        }

    }
    public void ShowAd()
    {
        Advertisement.Show("rewardedVideo");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("Unity Ads Error:" + message);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
                //continue game
                Debug.Log("Ad Finished");
                break;
            case ShowResult.Skipped:
                //Ad was skipped
                break;
            case ShowResult.Failed:
                Debug.Log("Ad Failed");
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Unity Ads Started");
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Unity Ads Ready");
    }
}
