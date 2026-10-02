using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInput
{
    // Mouse
    InputAction submitAction;
    InputAction cancelAction;

    // Camera
    InputAction moveCameraAction;
    InputAction rotateCameraAction;
    InputAction zoomCameraAction;

    public PlayerInput()
    {
        submitAction = InputSystem.actions["click"];
        cancelAction = InputSystem.actions["escape"];
        moveCameraAction = InputSystem.actions["move"];
        rotateCameraAction = InputSystem.actions["rotate"];
        zoomCameraAction = InputSystem.actions["scrollwheel"];
    }

    public MouseData MouseData { get; private set; } = new();
    public ClickData ClickData { get; private set; } = new();
    public CameraMovementData CameraData { get; private set; } = new();

    public void Update()
    {
        PopulateMouse();
        PopulateClick();
        PopulateCameraMovement();
    }

    // Mouse Input ============================================================================================
    /// <summary>
    /// Gather mouse related data
    /// </summary>
    private void PopulateMouse()
    {
        MouseData mouse = MouseData;

        mouse.ScreenPosition = Mouse.current.position.ReadValue();

        // UI Check
        mouse.isOverUI =
            EventSystem.current != null
            && EventSystem.current.IsPointerOverGameObject();

        if(mouse.isOverUI)
        {
            MouseData = mouse; return;
        }

        // World position and raycast hits
        Ray ray = Camera.main.ScreenPointToRay(mouse.ScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            mouse.WorldPosition = hit.point;

            mouse.Hit3D = hit;
        }


        MouseData = mouse;
    }
    private void PopulateClick()
    {
        ClickData click = ClickData;

        click.submitPressed = submitAction.WasPressedThisFrame();
        click.cancelPressed = cancelAction.WasPressedThisFrame();

        ClickData = click;
    }
    private void PopulateCameraMovement()
    {
        CameraMovementData camera = CameraData;

        camera.movement = moveCameraAction.ReadValue<Vector2>();
        camera.rotation = rotateCameraAction.ReadValue<Vector2>();
        camera.zoom = zoomCameraAction.ReadValue<Vector2>();

        CameraData = camera;
    }
}

public interface IClickInput
{
    public void OnClick();
}
public struct MouseData
{
    public Vector2 ScreenPosition;
    public Vector3 WorldPosition;
    public RaycastHit Hit3D;
    //public RaycastHit2D Hit2D;
    public bool isOverUI;
}
public struct ClickData
{
    public bool submitPressed;
    public bool cancelPressed;
}
public struct CameraMovementData
{
    public Vector2 movement;

    public Vector2 rotation;

    public Vector2 zoom;
}