using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    private const string newCarID = "com.eegames.eedriving.newcar";
    public const string newCarUnlockedKey = "NewCarUnlocked";
    GameObject buyButton;
    private readonly string[] carKeys = new string[] { "FirstCar", "SecondCar", "ThirdCar", "FourthCar", "FifthCar" };

    [SerializeField] private GameObject Cars;
    [SerializeField] private GameObject CameraLocations;

    static int currentCar;
    static bool currentCarUnlocked;

    GameObject unlockedImage;
    Camera mainCamera;

    private void Awake()
    {
        if(Application.platform != RuntimePlatform.IPhonePlayer)
        {
            //restore button set active false
        }
        currentCarUnlocked = true;
        unlockedImage = GameObject.Find("Canvas").GetComponent<Canvas>().transform.Find("unlockedImage").gameObject;
        buyButton = GameObject.Find("Canvas").GetComponent<Canvas>().transform.Find("BuyButton").gameObject; ;
        mainCamera = Camera.main;
        currentCar = 0;
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == newCarID )
        {
            unlockedImage.SetActive(true);
            PlayerPrefs.SetInt(carKeys[currentCar], 1);
        }
      
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.LogWarning("FAILED TO PURCHASE PRODUCT "+ product.definition.id + " because "+ reason);
    }


    public void changeCar(int whichSide)
    {

        if (whichSide == 1)
        {
            //check and go right
            if (currentCar < 4)
            {
                if (PlayerPrefs.GetInt(carKeys[currentCar + 1], 0) == 1)
                {
                    unlockedImage.SetActive(true);
                    buyButton.transform.GetComponent<Button>().enabled = false;
                    currentCarUnlocked = true;
                }
                else
                {
                    unlockedImage.SetActive(false);
                    buyButton.transform.GetComponent<Button>().enabled = true;
                    currentCarUnlocked = false;
                }
                currentCar++;
                mainCamera.transform.position = CameraLocations.transform.GetChild(currentCar).transform.position;

            }

        }
        if (whichSide == -1)
        {
            //check and go left
            if (currentCar > 0)
            {
                if (PlayerPrefs.GetInt(carKeys[currentCar - 1], 0) == 1)
                {
                    unlockedImage.SetActive(true);
                    buyButton.transform.GetComponent<Button>().enabled = false;
                    currentCarUnlocked = true;
                }
                else
                {
                    unlockedImage.SetActive(false);
                    buyButton.transform.GetComponent<Button>().enabled = true;
                    currentCarUnlocked = false;
                }
                currentCar--;
                mainCamera.transform.position = CameraLocations.transform.GetChild(currentCar).transform.position;
            }

        }
    }

}
