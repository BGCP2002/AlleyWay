using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StructureStat
{
    [SerializeField] internal BuildingStatType statType;
    [SerializeField] internal float statValue;
    [SerializeField] internal float revenueMod;
    [SerializeField] internal List<StructureAreaModifier> modifiers = new List<StructureAreaModifier>();

    /// <summary>
    /// Retrieve the sum of all modifiers
    /// </summary>
    public void SumModifiers()
    {
        statValue = 0;
        foreach (var mod in modifiers)
        {
            statValue += mod.value;
        }
    }

    /// <summary>
    /// Retrieve the revenue modifier
    /// </summary>
    public void GetRevenueMod(BuildingDefinition def)
    {
        revenueMod = 0;
        if (def == null) return;
        if (!def.effectors.Exists(x => x.type == statType)) return;
        SumModifiers();
        revenueMod = statValue * def.effectors.Find(x => x.type == statType).effectorPercentage;
    }
}