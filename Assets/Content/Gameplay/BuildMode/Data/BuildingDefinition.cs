using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Structure/Building Definition")]
public class BuildingDefinition : StructureDefinition
{
    //[Header("Egagement / Demographic / Population")]
    //[SerializeField] internal float ; // Engagement base stats

    [Header("Mantainance")]
    [SerializeField] internal float outcome;
    [SerializeField] internal float income;
    [SerializeField] internal List<StatEffector> effectors;

    internal override StructureInstance CreateInstance()
    {
        return new DecorInstance();
    }
}

public class BuildingInstance : StructureInstance
{

}

[System.Serializable]
public class StatEffector
{
    [SerializeField] internal BuildingStatType type;
    [SerializeField] internal float effectorPercentage;
}