using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildingDefinition : ScriptableObject
{
    [Header("Information")]
    [SerializeField] internal string displayName;
    [SerializeField] internal string description;

    [Header("Creation")]
    [SerializeField] internal Building prefab;
    //[SerializeField] internal List<building> startingModifiers;
    [SerializeField] internal float buildPrice;
    [SerializeField] internal float sellPrice;

    [Header("Mantainance")]
    [SerializeField] internal float upKeepCost;
    [SerializeField] internal float salePrice;
}
