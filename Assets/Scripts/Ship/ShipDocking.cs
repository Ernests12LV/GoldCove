using UnityEngine;

public class ShipDocking : MonoBehaviour
{
    public PlayerSpawner playerSpawner; // Reference to the PlayerSpawner script
    public int dockIndex; // The index of the current dock the ship is at

    public void OnDockingComplete(int Index)
    {
        Debug.Log($"Docking at dock {Index}.");
        playerSpawner.SpawnAtDock(Index); // Spawn the player at the correct dock
    }
}
