using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{
    public static BuildingHandler Instance;

    [SerializeField] private BuildMenu buildMenu;
    [SerializeField] private BuildModMenu buildModMenu;

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
        if (selectedPlot.buildingInstance != null)
        {
            buildModMenu.OpenModifiers(plot);
            buildMenu.OpenDemolish();
        }
        else
        {
            buildModMenu.OpenModifiers(plot);
            buildMenu.OpenBuildOptions();
        }
    }

    /// <summary>
    /// Instantiate a building and add it to the selected plot
    /// </summary>
    internal void AddBuilding(BuildingDefinition def)
    {
        // -> Check Cost

        Building building = Instantiate(def.prefab, selectedPlot.buildingParent);

        BuildingInstance newInstance = new BuildingInstance()
        {
            buildingDefinition = def,
            building = building,
        };
        selectedPlot.AddBuilding(newInstance);

        CloseMenus();
    }

    /// <summary>
    /// Remove a building from the selected plot
    /// </summary>
    internal void RemoveBuilding()
    {
        // -> Refund

        selectedPlot.RemoveBuilding();

        CloseMenus();
    }


    // Closing helpers
    internal void Cancel()
    {
        CloseMenus();
    }
    internal void CloseMenus()
    {
        buildMenu.Close();
        buildModMenu.Close();
    }
}
