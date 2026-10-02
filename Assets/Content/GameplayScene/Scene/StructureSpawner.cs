using System;
using System.Collections.Generic;
using UnityEngine;

public class StructureSpawner : MonoBehaviour
{
    [SerializeField] private Transform structureParent;

    public void Init(WorldSelection selection)
    {
        this.selection = selection;
    }
    private WorldSelection selection;

    private Dictionary<StructureInstance, GameObject> InstanceToObject = new();

    internal void Spawn(PlacementData data, StructureInstance instance)
    {
        GameObject structure = Instantiate(data.definition.prefab, data.worldPosition, Quaternion.identity, structureParent);
        InstanceToObject.Add(instance, structure);
        ClickBox clickBox = structure.GetComponentInChildren<ClickBox>();
        clickBox.OnCLick +=
            () => selection.SelectStructure(instance);
    }

    internal void Destroy(StructureInstance instance)
    {
        Destroy(InstanceToObject[instance]);
        InstanceToObject.Remove(instance);
    }
}
