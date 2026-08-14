using J_Func.Math;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlacementHandler : MonoBehaviour, IMouseInput, IClickInput, IRotationInput
{
    [Header("Components")]
    [SerializeField] private PlacementPreview PlacementPreview;
    [SerializeField] private StructureMenu StructureMenu;

    [Header("Runtime Storage")]
    [SerializeField] private Transform structureParent;
    private ValidationModule ValidationHandler = new();
    private StructureDataModule structureDataModule = new();
    private PlacementData PlacementData;

    private void Awake()
    {
        StructureMenu.OnStructureSelected += PlacementEnter;
    }


    // Structure interaction =============================================================================================
    internal void PlacementEnter(StructureDefinition definition)
    {
        PlacementPreview.DisablePreview();

        PlacementData = new()
        {
            definition = definition
        };

        PlacementPreview.EnablePreview(definition);
    }
    internal void PlacementExit()
    {
        PlacementData = null;
        PlacementPreview.DisablePreview();
    }

    internal void AttemptPlacement()
    {
        if (PlacementData == null) return;

        // Check valid placement position
        List<Vector2Int> list = ValidationHandler.GetOccupiedCells(PlacementData);
        if (!ValidationHandler.ValidateCells(list)) return;
        ValidationHandler.AddCells(list);

        // Create Instance
        //StructureInstance newInstance = definition.GreateInstance();
        //structureDataModule.structureInstances.Add(pos, instance);

        // Create Object
        GameObject newObj = Instantiate(PlacementData.definition.prefab, structureParent);
        newObj.transform.position = PlacementData.MouseWorldPosition;
    }
    internal void RemoveStructure()
    {

    }

    // Input Handling =============================================================================================
    public void UpdateMouseInfo(MouseInfo info)
    {
        if (PlacementData == null) return;

        Vector3Int pivotInt;
        if (PlacementData.definition is BuildingDefinition)
            pivotInt = J_Mathf.RoundToInt(info.WorldPosition, 5);
        else
            pivotInt = J_Mathf.RoundToInt(info.WorldPosition);

        PlacementData.mousePosition = new Vector2Int(pivotInt.x, pivotInt.z);

        PlacementPreview.UpdatePreview(PlacementData);
    }

    public void LeftClick(MouseInfo click)
    {
        AttemptPlacement();
    }

    public void RightClick(MouseInfo click)
    {

    }

    public void Rotation(float dir)
    { 

    }
}
