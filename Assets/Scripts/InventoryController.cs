using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController : MonoBehaviour
{
    private readonly string[] carKeys = new string[] { "FirstCar", "SecondCar", "ThirdCar", "FourthCar", "FifthCar" };
    private const string SelectedCar = "SelectedCar";

    [SerializeField] private GameObject Cars;
    [SerializeField] private GameObject CameraLocations;

    GameObject unlockedImage;
    Camera mainCamera;

    static int currentCar;
    static bool currentCarUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        currentCarUnlocked = true;
        unlockedImage = GameObject.Find("Canvas").GetComponent<Canvas>().transform.Find("LockedImage").gameObject;
        mainCamera = Camera.main;
        currentCar = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void changeCar(int whichSide)
    {
       
        if(whichSide == 1)
        {
            //check and go right
            if (currentCar < 4)
            {
                if (PlayerPrefs.GetInt(carKeys[currentCar+1],0) == 1)
                {
                    unlockedImage.transform.GetChild(0).gameObject.SetActive(false);
                    unlockedImage.transform.GetChild(1).gameObject.SetActive(false);
                    currentCarUnlocked = true;
                }
                else
                {
                    unlockedImage.transform.GetChild(0).gameObject.SetActive(true);
                    unlockedImage.transform.GetChild(1).gameObject.SetActive(true);
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
                    unlockedImage.transform.GetChild(0).gameObject.SetActive(false);
                    unlockedImage.transform.GetChild(1).gameObject.SetActive(false);
                    currentCarUnlocked = true;
                }
                else
                {
                     unlockedImage.transform.GetChild(0).gameObject.SetActive(true);
                     unlockedImage.transform.GetChild(1).gameObject.SetActive(true);
                    currentCarUnlocked = false;
                }
                currentCar--;
                mainCamera.transform.position = CameraLocations.transform.GetChild(currentCar).transform.position;
            }
         
        }
    }
    public void selectCar()
    {
        if (currentCarUnlocked)
        {
            Debug.Log("Selected Car : " + currentCar);
            PlayerPrefs.SetInt(SelectedCar,currentCar);
        }
    }
}
