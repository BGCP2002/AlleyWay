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
    [SerializeField] internal float buildPrice;
    [SerializeField] internal float sellPrice;

    [Header("Mantainance")]
    [SerializeField] internal float outcome;
    [SerializeField] internal float income;

    [Header("Income Effectors")]
    [SerializeField] internal List<PlotModifier> startingModifiers;
    [SerializeField] internal List<StatEffector> effectors;
}

[System.Serializable]
public class StatEffector
{
    [SerializeField] internal BuildingStatType type;
    [SerializeField] internal float effectorPercentage;
}