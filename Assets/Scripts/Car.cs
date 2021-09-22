using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    private const string UnlockedLevel = "LastUnlockedLevel";
    private const string CurrentPlayedLevel = "CurrentLevel";
    private const string CurrentDifficulty = "CurrentDifficulty";
    private const string SelectedCar = "SelectedCar";

    [SerializeField] private float speedOfCar = 30f;
    [SerializeField] private float acceleration = 0.25f;
    [SerializeField] private float turnSpeed = 150f;
    [SerializeField] private float diffIncrease = 0.05f;
    [SerializeField] private Material[] materials;

    private Rigidbody carRB;
    private bool stopped = false;
    float waitTime = 10;

    private int steerValue;



    private void Awake()
    {
        if (PlayerPrefs.GetInt(SelectedCar, 0) != 0)
        {
            GetComponent<MeshRenderer>().material = materials[PlayerPrefs.GetInt(SelectedCar, 0)];
        }
        startingPointAndPosition();
        
        float difficulty = PlayerPrefs.GetFloat(CurrentDifficulty);
        acceleration = acceleration + diffIncrease * difficulty;
       
       
    }
    // Start is called before the first frame update
    void Start()
    {
       
        carRB = transform.GetComponent<Rigidbody>();
        carRB.velocity = transform.forward * speedOfCar;
        if (PlayerPrefs.GetInt(Store.newCarUnlockedKey, 0) == 1)
        {
            GetComponentInChildren<Renderer>().material.SetColor("_Color",Color.blue);
        }



       
    }

    // Update is called once per frame
    void FixedUpdate()
    {



        if (!stopped)
        {
            transform.Rotate(0f, steerValue * turnSpeed * Time.deltaTime, 0f);

            speedOfCar += acceleration * Time.deltaTime;
            
            carRB.velocity = new Vector3( transform.forward.x * speedOfCar, carRB.velocity.y, transform.forward.z * speedOfCar);
            
        }
        

        
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            StartCoroutine(gameOver());
        }
        if (other.CompareTag("FinishLine"))
        {
            carRB.velocity = Vector3.zero;

            int finishedLevel = other.GetComponent<Identity>().getID();
            
            if ((finishedLevel+1)>PlayerPrefs.GetInt(UnlockedLevel,0))
            {
                PlayerPrefs.SetInt(UnlockedLevel, (finishedLevel + 1));
            }

            GameObject.Find("MainCanvas").GetComponent<Canvas>().transform.Find("GameplayScoreText").GetComponent<ScoreDisplay>().setCurScore();
            GameObject.Find("MainCanvas").GetComponent<Canvas>().transform.Find("GameplayScoreText").gameObject.SetActive(false);
            GameObject.Find("MainCanvas").GetComponent<Canvas>().transform.Find("WinPanel").gameObject.SetActive(true);


            stopped = true;
        }
      
       
    }
    private IEnumerator gameOver()
    {
       
        GameObject.Find("MainCanvas").GetComponent<Canvas>().transform.Find("GameplayScoreText").GetComponent<ScoreDisplay>().setCurScore();
        GameObject.Find("MainCanvas").GetComponent<Canvas>().transform.Find("GameplayScoreText").gameObject.SetActive(false);

        GameObject.Find("MainCanvas").GetComponent<Canvas>().transform.Find("LosePanel").gameObject.SetActive(true);
        stopped = true;
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Scene_MainMenu");
    }
    public void Steer(int value)
    {       
        steerValue = value;//by changing value, it means it turns left and right.
       
    }
   

    public void startingPointAndPosition()
    {
        //ACCORDING TO THE SELECTED LEVEL IT ARRANGE POSITION AND ROTATION OF CAR
        //IF USER DOESNT SELECT LEVEL FROM LEVEL SELECTION, PLAYS LAST UNLOCKED LEVEL
        int curPlayed = PlayerPrefs.GetInt(CurrentPlayedLevel, 0);
        GameObject nextSpawnPoint = GameObject.Find("SpawnPoints").transform.GetChild(curPlayed).gameObject;
        GameObject car = FindObjectOfType<Car>().gameObject;
        //PlayerPrefs.SetInt(CurrentPlayedLevel, curPlayed+1);



        switch (nextSpawnPoint.GetComponent<Identity>().getRotation())
        {
            case 1:
                car.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                car.GetComponent<Rigidbody>().rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            case 2:
                car.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                car.GetComponent<Rigidbody>().rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
                break;
            case 3:
                car.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                car.GetComponent<Rigidbody>().rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
                break;
            case 4:
                car.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
                car.GetComponent<Rigidbody>().rotation = Quaternion.Euler(new Vector3(0, -90f, 0));
                break;
        }
        car.transform.position = nextSpawnPoint.transform.position;
        car.GetComponent<Car>().setStopped(false);
    }

    public bool getStopped()
    {
        return stopped;
    }
    public void setStopped(bool value)
    {
        stopped = value;
    }
}
