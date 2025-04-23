using UnityEngine;

public class EnterShip : MonoBehaviour
{
    private bool playerInTriggerArea = false;
    public CameraManager cameraManager;
    public ShipManagement ShipManagement;
    private Transform dockParent;
    private Transform dockZone;
    private DockManager dockManager;
    private GameObject shipDocked;

    void Start(){
        InitalShipEnter();
    }

    void OnTriggerEnter(Collider other)
    {   
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {   
            dockParent = transform.parent;
            dockZone = dockParent.Find("ShipDockTrigger");
            dockManager = dockZone.GetComponent<DockManager>();
            shipDocked = dockManager.DockedShip;

            playerInTriggerArea = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the object that exited the trigger is the player
        if (other.CompareTag("Player"))
        {
            playerInTriggerArea = false;
        }
    }

    void Update()
    {
        // Check if the button is pressed and the player is inside the trigger area
        if (playerInTriggerArea && Input.GetKeyDown(KeyCode.E) && shipDocked)
        {
            //Debug.Log("Entering Ship");

            PlayerEnterShip();

            dockManager.UnDocking();
        }
    }

    public void PlayerEnterShip(){

        Debug.Log("Entering Ship");
        cameraManager.gameObject.SetActive(true);
        cameraManager.isPlayerView = false;

        GameObject existingPlayer = GameObject.FindWithTag("Player");
        

        if (existingPlayer != null)
        {
            existingPlayer.transform.SetParent(shipDocked.transform);

            Collider playerCollider = existingPlayer.GetComponent<Collider>();
            playerCollider.enabled = false;

            Rigidbody playerRigidbody = existingPlayer.GetComponent<Rigidbody>();
            playerRigidbody.isKinematic = true;
            playerRigidbody.linearVelocity = Vector3.zero; // Stop any ongoing motion

            // Move the existing player to the selected spawn point
            existingPlayer.transform.position = ShipManagement.playerInShip.transform.position;
            existingPlayer.transform.rotation = ShipManagement.playerInShip.transform.rotation;
        }

    }

    public void InitalShipEnter(){
        Debug.Log("Initial Entering Ship");
        cameraManager.gameObject.SetActive(true);
        cameraManager.isPlayerView = false;

        GameObject existingPlayer = GameObject.FindWithTag("Player");
        GameObject Ship = GameObject.Find("PlayerShip");
        

        if (existingPlayer != null)
        {
            existingPlayer.transform.SetParent(Ship.transform);

            Collider playerCollider = existingPlayer.GetComponent<Collider>();
            playerCollider.enabled = false;

            Rigidbody playerRigidbody = existingPlayer.GetComponent<Rigidbody>();
            playerRigidbody.isKinematic = true;
            playerRigidbody.linearVelocity = Vector3.zero; // Stop any ongoing motion

            // Move the existing player to the selected spawn point
            existingPlayer.transform.position = ShipManagement.playerInShip.transform.position;
            existingPlayer.transform.rotation = ShipManagement.playerInShip.transform.rotation;
        }
    }
}
