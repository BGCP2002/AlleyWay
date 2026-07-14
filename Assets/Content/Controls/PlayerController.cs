using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CameraController camControl;

    InputAction clickAction;
    InputAction rotateCameraAction;
    InputAction moveCameraAction;

    private void Awake()
    {
        clickAction = InputSystem.actions["click"];
        moveCameraAction = InputSystem.actions["move"];
        rotateCameraAction = InputSystem.actions["rotate"];
    }

    private void Update()
    {
        Click();
        CameraMovement();
        CameraRotation();
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
                Debug.Log(hit.transform.name);
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
            float movement = rotateCameraAction.ReadValue<float>();
            camControl.RotateCamera(movement);
        }
    }
}

public interface IClick
{
    public void OnClick();
}