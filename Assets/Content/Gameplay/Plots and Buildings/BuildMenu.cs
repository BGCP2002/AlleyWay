using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildMenu : Menu
{
    [SerializeField] private MenuOptionsHandler buildOptions;

    [SerializeField] private BuildingHandler buildingHandler;
    [SerializeField] private List<BuildingDefinition> definitions = new();


    public void OpenMenu(bool hasBuilding)
    {
        Open();

        if (hasBuilding)
        {
            buildOptions.AddMenuOption(new MenuOption()
            {
                displayText = "Demolish",
                onClickAction = () => buildingHandler.RemoveBuilding()
            });
        }
        else
        {
            foreach (var definition in definitions)
            {
                buildOptions.AddMenuOption(new MenuOption()
                {
                    displayText = definition.displayName,
                    onClickAction = () => buildingHandler.AddBuilding(definition)
                });
            }
        }
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        buildOptions.ClearOptions();
    }
    public override void Close()
    {
        gameObject.SetActive(false);
    }
}
