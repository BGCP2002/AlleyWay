using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Structure ( Building and Decor ) save data
/// </summary>
public class StructureDataModule
{
    internal Dictionary<Vector2Int, StructureInstance> StructureInstances { get; private set; }  = new();

    internal void AddStructure(StructureInstance structureInstance, Vector2Int vector2)
    {
        StructureInstances.Add(vector2, structureInstance);
    }
}
