using UnityEngine;

public class PlacementData
{
    internal Vector2Int mousePosition;
    internal Vector3Int MouseWorldPosition => new Vector3Int(mousePosition.x, 0, mousePosition.y);

    internal int rotation;

    internal StructureDefinition definition;

    internal bool isValid;
}
