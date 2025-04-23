using UnityEngine;

public class Building : MonoBehaviour
{
    private Tile currentTile;

    private void Start()
    {
        // Get the reference to the tile the building is placed on
        currentTile = GetComponentInParent<Tile>();
    }

    public void Demolish()
    {
        // Remove the building from the current tile
        //currentTile.RemoveBuilding();
        // Destroy the building gameObject
        Destroy(gameObject);
    }
}

