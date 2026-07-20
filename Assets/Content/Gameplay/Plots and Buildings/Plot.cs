using System;
using UnityEngine;

public class Plot : MonoBehaviour, IClick
{
    [SerializeField] internal Transform buildingParent;
    [SerializeField] internal BuildingInstance buildingInstance;
    [SerializeField] internal PlotSaveData saveData = new();

    private void Start()
    {
        saveData.position = transform.position;
    }

    /// <summary>
    /// Open menu showing interaction options
    /// </summary>
    public void OnClick()
    {
        BuildingHandler.Instance.SelectPlot(this);
        CameraController.Instance.SetFocus(transform);
    }

    /// <summary>
    /// Assign building to plot and initialise modifiers
    /// </summary>
    internal void AddBuilding(BuildingInstance buildingInstance)
    {
        this.buildingInstance = buildingInstance;

        saveData.modifierModule.AddModifiersFromDef(this, buildingInstance.buildingDefinition);
    }

    /// <summary>
    /// Remove the modifiers, destroy the gameobject, and clear the instance
    /// </summary>
    internal void RemoveBuilding()
    {
        saveData.modifierModule.RemoveModifiersFromDef(this, buildingInstance.buildingDefinition);

        Destroy(buildingInstance.building.gameObject);
        buildingInstance = null;
    }
}
