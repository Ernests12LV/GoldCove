using UnityEngine;

public class DockManager : MonoBehaviour
{
    public CameraManager cameraManager;   // Reference to the camera manager
    //public Transform dockedCameraPosition; // Reference to the camera's position while docked
    public PlayerMovement playerMovement;
    public Collider DockZone;
    public GameObject targetPosition;
    public float dockingSpeed = 5f;
    public int dockIndex;
    public bool isShipInDock = false;
    public GameObject DockedShip = null;


    private void Update(){
        if(Input.GetKeyDown(KeyCode.P) && isShipInDock){

            Docking();
        }
    }

    private void Docking(){
        //Debug.Log("Docking");

        ShipManagement shipManagement = DockedShip.GetComponent<ShipManagement>();
        ShipMovement shipMovement = DockedShip.GetComponent<ShipMovement>();
        ShipDocking shipDocking = DockedShip.GetComponent<ShipDocking>();

        //Debug.Log(DockedShip);
        DockedShip.transform.rotation = targetPosition.transform.rotation;
        DockedShip.transform.position = targetPosition.transform.position;
        //collidedObject.transform.position = Vector3.MoveTowards(collidedObject.transform.position, targetPosition.transform.position, dockingSpeed * Time.deltaTime);

        // Disable ship movement
        shipMovement.DisableMovement();

        playerMovement.EnableMovement();

        cameraManager.isPlayerView = true;
        cameraManager.gameObject.SetActive(false);

        shipManagement.DockShip();

        if (dockIndex >= 0)
        {
            shipDocking.OnDockingComplete(dockIndex);  // Spawn the player at the correct dock
        }
    }

    public void UnDocking(){

        //Debug.Log("Undocking");
        ShipManagement shipManagement = DockedShip.GetComponent<ShipManagement>();
        ShipMovement shipMovement = DockedShip.GetComponent<ShipMovement>();

        // Enable ship movement again
        shipMovement.EnableMovement();

        playerMovement.DisableMovement();

        // Switch camera back to ship mode or regular mode
        cameraManager.isPlayerView = false;

        Debug.Log("Docked Ship setting to null");
        DockedShip = null;
    } 

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entered DockTrigger");
        if (other.CompareTag("Ship")) // Check if the ship enters the docking area
        {   
            isShipInDock = true;
            GameObject collidedObject = other.gameObject;
            DockedShip = collidedObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exited DockTrigger");

        if (other.CompareTag("Ship")) // When the ship leaves the docking area
        {
            isShipInDock = false;
        }
    }

}