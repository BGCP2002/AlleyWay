using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using J_Func.UI;

public class BuildMenu : Menu
{
    [SerializeField] private MenuOptions buildOptions;

    [SerializeField] private BuildingHandler buildingHandler;
    [SerializeField] private List<BuildingDefinition> definitions = new();

    public void OpenBuildOptions()
    {
        OpenMenu();

        foreach (var definition in definitions)
        {
            buildOptions.AddMenuOption(new MenuOptionData()
            {
                displayText = definition.displayName,
                onClickAction = () => buildingHandler.AddBuilding(definition)
            });
        }
    }
    public void OpenDemolish()
    {
        OpenMenu();

        buildOptions.AddMenuOption(new MenuOptionData()
        {
            displayText = "Demolish",
            onClickAction = () => buildingHandler.RemoveBuilding()
        });
    }

    protected override void OpenMenu()
    {
        gameObject.SetActive(true);
        buildOptions.ClearOptions();
    }
    protected override void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
