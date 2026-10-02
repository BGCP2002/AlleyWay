using UnityEngine;

public class BaseState : IGameplayState
{
    public BaseState(GameServices gameServices, GameplayServices gameplayServices)
    {
        baseMenu = gameplayServices.BaseMenu;
        camera = gameplayServices.CameraController;
        raycaster = gameplayServices.WorldRaycaster;
        selection = gameplayServices.WorldSelection;
    }
    private readonly BaseMenu baseMenu;
    private readonly CameraController camera;
    private readonly WorldRaycaster raycaster;
    private readonly WorldSelection selection;

    public void OnEnter()
    {
        baseMenu.Open();

        //selection.SelectedStructure += camera.SetFocus;
    }

    public void OnExit()
    {
        //selection.SelectedStructure -= camera.SetFocus;

        baseMenu.Close();
    }

    public void Update()
    {
        //raycaster.DoClickInput();
    }

    public void FixedUpdate()
    {

    }
}
