using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static Unity.Collections.AllocatorManager;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    [SerializeField] private GameplayStateMachine GameStateMachine;
    [SerializeField] private CameraController camControl;

    InputAction clickAction;

    InputAction moveCameraAction;
    InputAction rotateCameraAction;
    InputAction zoomCameraAction;

    internal static Vector3 mouseScreenPosition {  get; private set; }
    internal static Vector3 mouseWorldPosition { get; private set; }

    private void Awake()
    {
        Instance = this;
        clickAction = InputSystem.actions["click"];

        moveCameraAction = InputSystem.actions["move"];
        rotateCameraAction = InputSystem.actions["rotate"];
        zoomCameraAction = InputSystem.actions["scrollwheel"];
    }

    private void Update()
    {
        MouseInputs();

        CameraMovement();
        CameraRotation();
        CameraZoom();
    }

    // Mouse Input ============================================================================================
    /// <summary>
    /// Gather mouse related data
    /// </summary>
    private MouseInfo GetMouseInfo()
    {
        // Mouse Positional Data
        MouseInfo mouseInfo = new MouseInfo();

        // Screen Position
        mouseScreenPosition = Mouse.current.position.ReadValue();
        mouseInfo.ScreenPosition = mouseScreenPosition;

        // UI Check
        mouseInfo.isOverUI =
            EventSystem.current != null
            && EventSystem.current.IsPointerOverGameObject();
        if (mouseInfo.isOverUI)
            return mouseInfo;

        // World position and raycast hits
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            mouseWorldPosition = hit.point;

            mouseInfo.Hit3D = hit;
        }
        mouseInfo.WorldPosition = mouseWorldPosition;

        return mouseInfo;
    }
    private void MouseInputs()
    {
        MouseInfo mouseInfo = GetMouseInfo();

        GameStateMachine.UpdateMouseInfo(mouseInfo);

        if (clickAction.WasPressedThisFrame())
        {
            Debug.Log("Clicked!");
            GameStateMachine.LeftClick(mouseInfo);
        }
    }

    // Camera ============================================================================================
    /// <summary>
    /// Move the camera position manually
    /// </summary>
    private void CameraMovement()
    {
        if (moveCameraAction.IsPressed())
        {
            Vector2 movement = moveCameraAction.ReadValue<Vector2>();
            camControl.MoveCamera(movement);
        }
    }   
    
    /// <summary>
    /// Move the camera rotation manually
    /// </summary>
    private void CameraRotation()
    {
        if (rotateCameraAction.IsPressed())
        {
            Vector2 rotation = rotateCameraAction.ReadValue<Vector2>();
            camControl.ChangeTargetRotation(rotation);
        }
    }

    /// <summary>
    /// Move the camera zoom manually
    /// </summary>
    private void CameraZoom()
    {
        if (zoomCameraAction.IsPressed())
        {
            Vector2 zoom = zoomCameraAction.ReadValue<Vector2>();
            camControl.ChangeTargetZoom(zoom.y);
        }
    }
}

public interface IClick // -> Soon to be depreciated?
{
    public void OnClick();
}
public interface IMouseInput
{
    void UpdateMouseInfo(MouseInfo info);
}

public interface IClickInput
{
    void LeftClick(MouseInfo click);
    void RightClick(MouseInfo click);
}
public struct MouseInfo
{
    public Vector2 ScreenPosition;
    public Vector3 WorldPosition;
    public RaycastHit Hit3D;
    //public RaycastHit2D Hit2D;
    public bool isOverUI;
}

public interface IRotationInput
{
    void Rotation(float dir);
}


public interface INavigationInput // -> Temp ( Ignore this )
{
    internal abstract void Move(float dir);
    internal abstract void Zoom(float dir);
    internal abstract void Cancel();
}