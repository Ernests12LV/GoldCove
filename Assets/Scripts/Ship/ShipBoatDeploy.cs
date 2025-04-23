using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBoatDeploy : MonoBehaviour
{
    public GameObject boatModel;
    public GameObject boatDropPoint;
    private GameObject boat;

    public bool boatAvailable = false;
    public bool boatEnabled = false;

    void Start()
    {
        
    }

    void Update()
    {
        checkIfBoatDeployed();
        if (boatAvailable)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                //Switch To boat
                SpawnBoat();

            }
        }
    }

    public void SpawnBoat()
    {
        boat = Instantiate(boatModel, boatDropPoint.transform);
    }

    private void checkIfBoatDeployed()
    {
        if (boat != null)
        {
            boatAvailable = false;

            boatEnabled = boatAvailable;
        }
        else
        {
            boatAvailable = true;

            boatEnabled = boatAvailable;
        }
    }
}
