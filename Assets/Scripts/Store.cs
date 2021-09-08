using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
public class Store : MonoBehaviour
{
    private const string newCarID = "com.eegames.eedriving.newcar";
    public const string newCarUnlockedKey = "NewCarUnlocked";
    private void Awake()
    {
        if(Application.platform != RuntimePlatform.IPhonePlayer)
        {
            //restore button set active false
        }
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == newCarID )
        {
            PlayerPrefs.SetInt(newCarUnlockedKey, 1);
        }
      
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.LogWarning("FAILED TO PURCHASE PRODUCT "+ product.definition.id + " because "+ reason);
    }

}
