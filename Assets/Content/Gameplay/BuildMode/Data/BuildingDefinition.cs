using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Structure/Building_Definition")]
public class BuildingDefinition : StructureDefinition
{
    //[Header("Egagement / Demographic / Population")]
    //[SerializeField] internal float ; // Engagement base stats

    [Header("Mantainance")]
    [SerializeField] internal float outcome;
    [SerializeField] internal float income;
    [SerializeField] internal List<StatEffector> effectors;
}

[System.Serializable]
public class StatEffector
{
    [SerializeField] internal BuildingStatType type;
    [SerializeField] internal float effectorPercentage;
}