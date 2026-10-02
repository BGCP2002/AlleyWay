using J_Func.Math;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementHandler
{
    public PlacementHandler(PlayerInput input, StructureSpawner spawner,PlacementValidation validation, PlacementPreview preview, BuildSelection selection)
    {
        this.input = input;
        this.spawner = spawner;
        this.validation = validation;
        this.preview = preview;

        preview.Disable();

        selection.StructureSelected += SelectStructure;
    }
    private readonly PlayerInput input;
    private readonly StructureSpawner spawner;
    private readonly PlacementValidation validation;
    private readonly PlacementPreview preview;

    private StructureDefinition definition;

    // Structure interaction =============================================================================================
    internal void SelectStructure(StructureDefinition definition)
    {
        this.definition = definition;

        preview.SetDefinition(definition);
    }

    internal void CreateStructure(PlacementData data)
    {
        List<Vector2Int> list = validation.GetOccupiedCells(data);
        validation.AddCells(list);

        // Create Instance
        StructureInstance instance = data.definition.CreateInstance();

        // Create Object
        spawner.Spawn(data, instance);
    }

    public void Tick()
    {
        if (definition == null) return;

        PlacementData data = new()
        {
            isValid = true,
            definition = definition
        };
        data.worldPosition = RoundPosition(data, input.MouseData.WorldPosition);

        // Validation
        List<Vector2Int> list = validation.GetOccupiedCells(data);
        if (!validation.ValidateCells(list))
        {
            data.isValid = false;
        }
        if (data.IsBuilding)
        {
            if (Mathf.Abs(data.CellPosition.y) != 10)
                data.isValid = false;
        }

        preview.UpdatePreview(data);

        if (!data.isValid) return;

        if (input.ClickData.submitPressed)
        {
            CreateStructure(data);
        }
    }

    private Vector3Int RoundPosition(PlacementData data, Vector3 position)
    {
        if (data.IsBuilding)
            return J_Mathf.RoundToInt(position, 5);
        else
            return J_Mathf.RoundToInt(position);
    }

    internal void OnExit()
    {
        preview.Disable();
        definition = null;
    }
}
public struct PlacementData
{
    public StructureDefinition definition;
    public readonly bool IsBuilding => definition is BuildingDefinition;

    public bool isValid;

    public Vector3 worldPosition;
    public Vector2Int CellPosition
    {
        get
        {
            Vector3Int worldPositionInt = J_Mathf.RoundToInt(worldPosition);
            return new Vector2Int(worldPositionInt.x, worldPositionInt.z);
        }
    }
}