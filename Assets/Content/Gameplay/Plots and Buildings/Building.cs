using UnityEngine;

public class Building : MonoBehaviour, IClick
{
    internal Plot parentPlot;

    private void Awake()
    {
        parentPlot = GetComponentInParent<Plot>();
    }

    public void OnClick()
    {
        CameraController.Instance.SetFocus(new()
        {
            target = transform,
        });
        BuildingHandler.Instance.SelectPlot(parentPlot);
    }
}
