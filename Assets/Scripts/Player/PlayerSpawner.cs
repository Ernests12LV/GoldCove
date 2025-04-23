using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{   
    public GameObject playerPrefab; // Reference to the player prefab
    public Transform[] spawnPoints; // Array of dock spawn points

    // Method to spawn the player at a specific dock (index)
    public void SpawnAtDock(int dockIndex)
    {
        Debug.Log("Spawning...");
        if (dockIndex < 0 || dockIndex >= spawnPoints.Length)
        {
            Debug.LogWarning("Invalid dock index.");
            return;
        }

        Transform spawnPoint = spawnPoints[dockIndex];
        //Debug.Log($"Spawning player at dock {dockIndex}.");

        // Check if the player already exists
        GameObject existingPlayer = GameObject.FindWithTag("Player");
        //Debug.Log(existingPlayer);
        if (existingPlayer != null)
        {
            existingPlayer.transform.SetParent(null);

            Collider playerCollider = existingPlayer.GetComponent<Collider>();
            playerCollider.enabled = true;

            Rigidbody playerRigidbody = existingPlayer.GetComponent<Rigidbody>();
            playerRigidbody.isKinematic = false;
            //playerRigidbody.linearVelocity = Vector3.zero;

            // Move the existing player to the selected spawn point
            existingPlayer.transform.position = spawnPoint.position;
            existingPlayer.transform.rotation = spawnPoint.rotation;
        }
        else
        {
            // Instantiate a new player at the selected spawn point
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
