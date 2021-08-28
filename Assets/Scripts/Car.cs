using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float speedOfCar = 10f;
    [SerializeField] private float acceleration = 0.25f;
    [SerializeField] private float turnSpeed = 200f;
    private int steerValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, steerValue * turnSpeed * Time.deltaTime, 0f);
        transform.Translate(Vector3.forward * speedOfCar * Time.deltaTime);
        speedOfCar += acceleration * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene("Scene_MainMenu");
            speedOfCar = 0f;
            acceleration = 0f;
        }
       
    }
    public void Steer(int value)
    {
        steerValue = value;//by changing value, it means it turns left and right.
       
    }
   
}
