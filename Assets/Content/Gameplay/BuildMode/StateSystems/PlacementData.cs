using UnityEngine;

public class PlacementData
{
    internal Vector2Int mousePosition;
    internal Vector3Int MouseGridPosition => new Vector3Int(mousePosition.x, 0, mousePosition.y);
    internal Vector2Int MouseDataPosition => new Vector2Int(mousePosition.x, mousePosition.y);

    internal int rotation;

    internal StructureDefinition definition;

    internal bool isValid;
}
