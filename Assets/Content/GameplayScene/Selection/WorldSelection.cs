using System;
using UnityEngine;

public class WorldSelection
{
    public event Action<StructureInstance> StructureSelected;
    public StructureInstance SelectedStructure { get; private set; }
    public void SelectStructure(StructureInstance instance)
    {
        SelectedStructure = instance;
        StructureSelected?.Invoke(instance);
    }
}
