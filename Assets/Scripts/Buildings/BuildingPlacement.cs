using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    public Building buildingPrefab;

    private Tile selectedTile;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Tile hitTile = hit.collider.GetComponent<Tile>();

                if (hitTile != null)
                {
                    // Deselect the previous selected tile
                    if (selectedTile != null)
                        selectedTile.SetAvailability(true);

                    // Set the new selected tile
                    selectedTile = hitTile;
                    selectedTile.SetAvailability(false);
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && selectedTile != null)
        {
            // Place the building on the selected tile
            selectedTile.PlaceBuilding(buildingPrefab);
            selectedTile = null;
        }
    }
}
