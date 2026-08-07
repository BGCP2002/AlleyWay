using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Structure/Definition")]
public class StructureDefinition : ScriptableObject
{
    [Header("Information")]
    [SerializeField] internal string displayName;
    [SerializeField] internal string description;

    [Header("Placement")]
    [SerializeField] internal GameObject prefab;
    [SerializeField] internal Vector2Int size = Vector2Int.one;
    [SerializeField] internal bool canRotate;

    [Header("Economy")]
    [SerializeField] internal float buildPrice;
    [SerializeField] internal float sellPrice;

    [Header("Effectors")]
    [SerializeField] internal List<PlotModifier> startingModifiers;
}

public class StructureInstance
{

}
