using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacementHandler : MonoBehaviour
{
    Dictionary<Vector2Int, StructureInstance> occupiedTiles;

    [SerializeField] private bool buildModeEnabled;
    [SerializeField] private Transform placementGuide;

    private void Update()
    {
        placementGuide.gameObject.SetActive(buildModeEnabled);
        if (buildModeEnabled)
        {
            placementGuide.position = J_Mathf.RoundToVector3Int(PlayerController.mouseWorldPosition);
        }
    }
}

public class StructurePlacementModule
{

}