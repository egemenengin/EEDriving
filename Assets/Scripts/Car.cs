using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float speedOfCar = 30f;
    [SerializeField] private float acceleration = 0.25f;
    [SerializeField] private float turnSpeed = 200f;
    private bool crushed = false;
    float waitTime = 2;

    private int steerValue;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<Rigidbody>().velocity = transform.forward * speedOfCar;
        if (PlayerPrefs.GetInt(Store.newCarUnlockedKey, 0) == 1)
        {
            GetComponentInChildren<Renderer>().material.SetColor("_Color",Color.blue);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0f, steerValue * turnSpeed * Time.deltaTime, 0f);
        
        
        //transform.Translate(Vector3.forward * speedOfCar * Time.deltaTime);
        //transform.GetComponent<Rigidbody>().AddTorque(transform.up*steerValue * turnSpeed * Time.deltaTime);
        //transform.GetComponent<Rigidbody>().AddForce(transform.up * steerValue * turnSpeed * Time.deltaTime);


        if (!crushed)
        {
            Debug.Log(transform.forward);
            speedOfCar += acceleration * Time.deltaTime;
            transform.GetComponent<Rigidbody>().velocity =new Vector3( transform.forward.x* speedOfCar, transform.GetComponent<Rigidbody>().velocity.y, transform.forward.z * speedOfCar);
            
        }
        

        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            FindObjectOfType<Canvas>().transform.Find("WarningText").gameObject.SetActive(true);
            StartCoroutine(gameOver());

            //FindObjectOfType<Canvas>().transform.Find("Left").gameObject.SetActive(false);
            //FindObjectOfType<Canvas>().transform.Find("Right").gameObject.SetActive(false);
            //speedOfCar = 0f;
            //acceleration = 0f;
        }
        if (other.CompareTag("Wall"))
        {
            
            StartCoroutine(gameOver());
        }
       
    }
    private IEnumerator gameOver()
    {
        FindObjectOfType<Canvas>().transform.Find("WarningText").gameObject.SetActive(true);
        crushed = true;
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Scene_MainMenu");
    }
    public void Steer(int value)
    {
       
        steerValue = value;//by changing value, it means it turns left and right.
       
    }
   
}
