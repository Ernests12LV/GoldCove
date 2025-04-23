using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;
    public GameObject shipObject;

    public float turnSpeed = 4.0f;
    public float zoomSpeed = 8.0f;
    public float FPoffset = 10f;
    public float minZoomDistance = 7.5f;
    public float maxZoomDistance = 50f;
    public float minTurnAngle = 0.0f;
    public float maxTurnAngle = 90.0f;

    private float targetShipDistance;
    private float playerDistance;

    private float rotX;
    private float rotY;

    public bool isPlayerView = false;

    public bool isMovementEnabled = true;

    private void Update()
    {

        ShipManagement shipManagement = shipObject.GetComponent<ShipManagement>();

        // Handle camera switching between ship and docked views
        if (!isPlayerView)
        {   
            gameObject.SetActive(true);
            SwitchToShipCamera();
        }

        if (shipManagement.isShipDocked && !isPlayerView) //Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.E)  && 
        {
            //gameObject.SetActive(false);
            //HandleFPSView();
            SwitchToShip();
        }
    }
    
    public void SwitchToShip()
    {       
            ShipManagement shipManagement = shipObject.GetComponent<ShipManagement>();
            isPlayerView = false;
            EnableShipMovement();
            shipManagement.UnDockShip();
            
        }

    public void SwitchToShipCamera(){
        gameObject.SetActive(true);
        HandleShipCameraMovement();
    }

    private void HandleShipCameraMovement()
    {
        rotY = Input.GetAxis("Mouse X") * turnSpeed;
        rotX -= Input.GetAxis("Mouse Y") * turnSpeed;
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);

        float zoomDelta = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        targetShipDistance = Mathf.Clamp(targetShipDistance - zoomDelta, minZoomDistance, maxZoomDistance);

        transform.eulerAngles = new Vector3(rotX, transform.eulerAngles.y + rotY, 0);
        transform.position = target.position - (transform.forward * targetShipDistance);
    }

    private void EnableShipMovement()
    {

        if (shipObject != null)
        {
            ShipMovement shipMovement = shipObject.GetComponent<ShipMovement>();

            if (shipMovement != null)
            {
                shipMovement.EnableMovement();
            }
        }

        GameObject playerHeadObject = GameObject.Find("Character");

        if (playerHeadObject != null)
        {
            PlayerMovement playerMovement = playerHeadObject.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.DisableMovement();
            }
        }
    }

}
