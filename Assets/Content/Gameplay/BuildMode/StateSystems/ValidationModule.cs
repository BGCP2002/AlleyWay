using System.Collections.Generic;
using UnityEngine;

public class ValidationModule 
{
    private List<Vector2Int> blockedPositions = new();

    internal List<Vector2Int> GetOccupiedCells(PlacementData data)
    {
        List<Vector2Int> cells = new();

        for (int x = 0; x < data.definition.size.x; x++)
        {
            for (int y = 0; y < data.definition.size.y; y++)
            {
                cells.Add(data.pivot + new Vector2Int(x, y));
            }
        }

        return cells;
    }

    internal bool ValidateCells(List<Vector2Int> cells)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if (blockedPositions.Contains(cells[i]))
                return false;
        }
        return true;
    }

    internal void AddCells(List<Vector2Int> cells)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            blockedPositions.Add(cells[i]);
        }
    }
}
