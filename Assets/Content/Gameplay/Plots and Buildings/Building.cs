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
        BuildingHandler.Instance.SelectPlot(parentPlot);
    }
}
