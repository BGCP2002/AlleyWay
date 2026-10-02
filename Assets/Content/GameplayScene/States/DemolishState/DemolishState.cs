using UnityEngine;

public class DemolishState : IGameplayState
{
    public DemolishState(GameplaySceneController sceneController, GameServices gameServices, GameplayServices gameplayServices)
    {
        this.sceneController = sceneController;

        input = gameServices.PlayerInput;

        spawner = gameplayServices.StructureSpawner;
        selection = gameplayServices.WorldSelection;
        raycaster = gameplayServices.WorldRaycaster;
    }
    private readonly GameplaySceneController sceneController;
    private readonly PlayerInput input;
    private readonly StructureSpawner spawner;
    private readonly WorldSelection selection;
    private readonly WorldRaycaster raycaster;

    public void OnEnter()
    {
        selection.StructureSelected += spawner.Destroy;
    }

    public void OnExit()
    {
        selection.StructureSelected -= spawner.Destroy;
    }

    public void Update()
    {
        if (input.ClickData.cancelPressed)
        {
            sceneController.EnterBaseMode(); return;
        }

        raycaster.DoClickInput();
    }

    public void FixedUpdate()
    {

    }
}
