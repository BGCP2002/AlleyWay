using UnityEngine;

[System.Serializable]
public class PlotSaveData
{
    [SerializeField] internal Vector3 position;
    [SerializeField] internal string buildingDefinitionID;
    [SerializeField] internal PlotModifierModule modifierModule = new();
}

