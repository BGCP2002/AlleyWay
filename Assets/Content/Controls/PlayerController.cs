using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CameraController camControl;

    InputAction clickAction;

    // Camera ( Free mode )
    InputAction moveCameraAction;
    InputAction rotateCameraAction;
    InputAction zoomCameraAction;

    private void Awake()
    {
        clickAction = InputSystem.actions["click"];
        moveCameraAction = InputSystem.actions["move"];
        rotateCameraAction = InputSystem.actions["rotate"];
        zoomCameraAction = InputSystem.actions["scrollwheel"];
    }

    private void Update()
    {
        Click();
        CameraMovement();
        CameraRotation();
        CameraZoom();
    }

    /// <summary>
    /// Mouse Click on 3D components
    /// </summary>
    private void Click()
    {
        if (clickAction.WasPressedThisFrame())
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return; // Mouse is over UI
            }

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
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
            float rotation = rotateCameraAction.ReadValue<float>();
            camControl.RotateCamera(rotation);
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
            camControl.ChangeZoom(zoom.y);
        }
    }
}

public interface IClick
{
    public void OnClick();
}