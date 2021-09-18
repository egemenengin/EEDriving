using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identity : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] int rotation;
    [SerializeField] private bool isThatCheckPoint;
    [SerializeField] private bool isThatSpawnPoint;
    public int getID()
    {
        return id;
    }
    public int getRotation()
    {
        return rotation;
    }
    public bool getIsThatCheckpoint()
    {
        return isThatCheckPoint;
    }
    public bool getIsThatSpawnPoint()
    {
        return isThatSpawnPoint;
    }
}
