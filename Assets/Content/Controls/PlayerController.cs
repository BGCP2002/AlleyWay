using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

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
        MouseDelta(); // < - Must be called before mouse interactions in this script at least once

        MouseClick();
        CameraMovement();
        CameraRotation();
        CameraZoom();
    }

    // Mouse Input ============================================================================================
    /// <summary>
    /// Mouse Click on 3D components
    /// </summary>
    private void MouseClick()
    {
        if (clickAction.WasPressedThisFrame())
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return; // Mouse is over UI
            }

            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                mouseWorldPosition = hit.point;

                Debug.Log($"Player clicked: {hit.collider.name}");
                if (hit.transform.TryGetComponent(out IClick clickable))
                {
                    clickable.OnClick();
                    return;
                }
            }

            // No target hit
            BuildingHandler.Instance.Cancel();
        }
    }    
    
    /// <summary>
    /// Retrieve the mouse screen and world positions
    /// </summary>
    private void MouseDelta()
    {
        mouseScreenPosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            mouseWorldPosition = hit.point;
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

public interface IClick
{
    public void OnClick();
}