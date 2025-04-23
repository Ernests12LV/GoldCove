using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShipManagement : MonoBehaviour
{
    public bool isShipDocked;
    public Transform playerInShip;

    private void checkIfShipDocked(){
        //Debug.Log("Is Ship Docked: "+isShipDocked);
    } 

    public void DockShip(){
        isShipDocked = true;
        //Debug.Log("Ship Docked");
    }

    public void UnDockShip(){
        isShipDocked = false;
        //Debug.Log("Ship Undocked");
    }

    private void EnableShipMovement()
    {
        GameObject objCamera = GameObject.Find("ShipCamera");

        CameraManager cameraManager = objCamera.GetComponent<CameraManager>();

        GameObject shipObject = cameraManager.shipObject;

        if (shipObject != null)
        {
            ShipMovement shipMovement = shipObject.GetComponent<ShipMovement>();

            if (shipMovement != null)
            {
                shipMovement.EnableMovement();
            }
        }
    }

    private void DisableShipMovement()
    {
        GameObject objCamera = GameObject.Find("ShipCamera");

        CameraManager cameraManager = objCamera.GetComponent<CameraManager>();

        GameObject shipObject = cameraManager.shipObject;

        if (shipObject != null)
        {
            ShipMovement shipMovement = shipObject.GetComponent<ShipMovement>();

            if (shipMovement != null)
            {
                shipMovement.DisableMovement();
            }
        }
    }

}
