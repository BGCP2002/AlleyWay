using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CameraController camControl;

    InputAction clickAction;
    InputAction moveAction;

    private void Awake()
    {
        clickAction = InputSystem.actions["click"];
        moveAction = InputSystem.actions["move"];
    }

    private void Update()
    {
        Click();
        CameraMovement();
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
                }
            }
        }
    }

    /// <summary>
    /// Move the camera position manually
    /// </summary>
    private void CameraMovement()
    {
        if (moveAction.IsPressed())
        {
            Vector2 movement = moveAction.ReadValue<Vector2>();
            camControl.MoveCamera(movement);
        }
    }
}

public interface IClick
{
    public void OnClick();
}