using J_Func.Math;
using J_Func.UI;
using System.Collections.Generic;
using UnityEngine;

public class BuildStateManager : GameState, IClickInput, IRotationInput, IMouseInput
{
    private StructureDataModule dataModule;
    private bool buildModeEnabled = false;

    [Header("Visual Representation")]
    [SerializeField] private MenuHandler MenuHandler;

    [Header("Components")]
    private PlacementHandler PlacementHandler;

    [Header("Placement Data")]
    [SerializeField] private StructureDefinition structureDefinition;

    private void Awake()
    {
        PlacementHandler = GetComponentInChildren<PlacementHandler>();
    }

    private void Update()
    {
    }

    // State Handling =============================================================================================
    internal override void OnEnter()
    {
        if (buildModeEnabled) return;

        buildModeEnabled = true;
        MenuHandler.OpenMenus();
    }

    internal override void OnExit()
    {
        if (!buildModeEnabled) return;

        buildModeEnabled = false;
        MenuHandler.CloseMenus();
    }

    // Input Handling =============================================================================================

    public void UpdateMouseInfo(MouseInfo info)
    {
        PlacementHandler.UpdateMouseInfo(info);
    }

    public void LeftClick(MouseInfo click)
    {
        PlacementHandler.LeftClick(click);
    }

    public void RightClick(MouseInfo click)
    {
        PlacementHandler.RightClick(click);
    }

    public void Rotation(float dir)
    {
        // -> Will allow rotation for decor
    }

}