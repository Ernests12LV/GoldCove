using UnityEngine;

public class Tile : MonoBehaviour
{
    public new Renderer renderer;
    public Color availableColor = Color.white;
    public Color unavailableColor = Color.red;
    public bool isAvailable;
    private Building currentBuilding;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void SetAvailability(bool _isAvailable)
    {
        isAvailable = _isAvailable;
        if (isAvailable)
            renderer.material.color = availableColor;
        else
            renderer.material.color = unavailableColor;
    }

    public void PlaceBuilding(Building prefab)
    {
        // Check if there is already a building on this tile
        if (currentBuilding != null)
        {
            Debug.Log("Building already exists on this tile!");
            return;
        }

        // Instantiate the building prefab and position it on the tile
        Building newBuilding = Instantiate(prefab, transform.position, Quaternion.identity);
        currentBuilding = newBuilding;
    }
}
