using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class StructureDefinition : ScriptableObject
{
    [Header("Information")]
    [SerializeField] internal string displayName;
    [SerializeField] internal string description;

    [Header("Placement")]
    [SerializeField] internal GameObject prefab;
    [SerializeField] private Shape shape;
    [SerializeField] private List<Vector2Int> positions = new();
    [SerializeField] internal bool canRotate;

    [Header("Economy")]
    [SerializeField] internal float buildPrice;
    [SerializeField] internal float sellPrice;

    [Header("Effectors")]
    [SerializeField] internal List<StructureAreaModifier> startingModifiers;

    internal List<Vector2Int> GetPositions()
    {
        if (shape == Shape.Custom)
            return positions;

        int size = shape switch
        {
            Shape.Square2x2 => 2,
            Shape.Square3x3 => 3,
            Shape.Square4x4 => 4,
            Shape.Square5x5 => 5,
            _ => 0
        };

        List<Vector2Int> result = new();

        Vector2Int offset = new Vector2Int(size / 2, size / 2);

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                result.Add(new Vector2Int(x, y) - offset);
            }
        }

        return result;
    }

    internal abstract StructureInstance CreateInstance();
}

public class StructureInstance
{

}

public enum Shape
{
    Custom,
    Square2x2,
    Square3x3,
    Square4x4,
    Square5x5
}

