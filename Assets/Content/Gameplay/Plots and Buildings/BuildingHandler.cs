using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{
    public static BuildingHandler Instance;

    [SerializeField] private BuildMenu menu;

    private Plot selectedPlot;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Open build menu
    /// </summary>
    internal void SelectPlot(Plot plot)
    {
        selectedPlot = plot;
        menu.OpenMenu(plot.buildingInstance != null);
    }

    /// <summary>
    /// Instantiate a building and add it to the selected plot
    /// </summary>
    internal void AddBuilding(BuildingDefinition def)
    {
        // -> Check Cost

        Building building = Instantiate(def.prefab, selectedPlot.buildingParent);
        selectedPlot.buildingInstance = new BuildingInstance()
        {
            buildingDefinition = def,
            building = building,
        };

        menu.Close();
    }

    /// <summary>
    /// Remove a building from the selected plot
    /// </summary>
    internal void RemoveBuilding()
    {
        // -> Refund

        Destroy(selectedPlot.buildingInstance.building.gameObject);
        selectedPlot.buildingInstance = null;

        menu.Close();
    }
}
