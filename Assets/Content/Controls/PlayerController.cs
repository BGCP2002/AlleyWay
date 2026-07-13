using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputAction clickAction;

    private void Awake()
    {
        clickAction = InputSystem.actions["click"];
    }

    private void Update()
    {
        Click();
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
}

public interface IClick
{
    public void OnClick();
}