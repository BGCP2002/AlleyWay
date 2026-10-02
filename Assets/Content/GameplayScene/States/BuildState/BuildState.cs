using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows;

public class BuildState : IGameplayState
{
    public BuildState(GameplaySceneController sceneController, GameServices gameServices, GameplayServices gameplayServices)
    {
        this.sceneController = sceneController;

        structureMenu = gameplayServices.StructureMenu;
        placement = gameplayServices.PlacementHandler;

        input = gameServices.PlayerInput;
    }
    private readonly GameplaySceneController sceneController;
    private readonly StructureMenu structureMenu;
    private readonly PlacementHandler placement;
    private readonly PlayerInput input;

    public void OnEnter()
    {
        structureMenu.Open();
    }

    public void OnExit()
    {
        placement.OnExit();

        structureMenu.Close();
    }

    public void Update()
    {
        if (input.ClickData.cancelPressed)
        {
            sceneController.EnterBaseMode(); return;
        }

        placement.Tick();
    }

    public void FixedUpdate()
    {

    }
}