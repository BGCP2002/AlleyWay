using UnityEngine;

public class Plot : MonoBehaviour, IClick
{
    [SerializeField] internal Transform buildingParent;
    internal BuildingInstance buildingInstance;

    /// <summary>
    /// Open menu showing interaction options
    /// </summary>
    public void OnClick()
    {
        BuildingHandler.Instance.SelectPlot(this);
    }
}
