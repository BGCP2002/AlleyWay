using UnityEngine;

public class WorldRaycaster
{
    public WorldRaycaster(PlayerInput input)
    {
        this.input = input;
    }
    private readonly PlayerInput input;

    public void DoClickInput()
    {
        if (!input.ClickData.submitPressed) return;

        Ray ray = Camera.main.ScreenPointToRay(input.MouseData.ScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out IClickInput clickable))
            {
                clickable.OnClick();
            }
        }
    }
}
