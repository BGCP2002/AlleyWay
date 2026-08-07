using UnityEngine;

public class PlacementData
{
    internal Vector2Int pivot;
    internal Vector3Int WorldPivot => new Vector3Int(pivot.x, 0, pivot.y);

    internal int rotation;

    internal StructureDefinition definition;

    internal bool isValid;
}
