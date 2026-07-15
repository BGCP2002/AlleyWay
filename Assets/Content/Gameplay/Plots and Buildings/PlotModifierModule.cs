using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[System.Serializable]
public class PlotModifierModule
{
    internal List<BuildingStat> stats { get; private set; } = new();
    private bool isDirty = true;

    /// <summary>
    /// Adds all modifiers from a definition to its neighbours
    /// </summary>
    internal void AddModifiersFromDef(Plot plot, BuildingDefinition def)
    {
        foreach (PlotModifier mod in def.startingModifiers)
        {
            mod.source = "from nearby " + plot.buildingInstance.buildingDefinition.displayName;

            foreach (var p in PlotHandler.Instance.GetNeighbours(plot, mod.range))
            {
                p.saveData.modifierModule.AddModifier(mod);
            }
        }
    }

    /// <summary>
    /// Adds a modifier to a stat, creating the stat if its missing
    /// </summary>
    internal void AddModifier(PlotModifier mod)
    {
        isDirty = true;

        foreach (BuildingStat stat in stats)
        {
            if (stat.statType == mod.modType)
            {
                stat.modifiers.Add(mod);
                return;
            }
        }

        // If stat doesnt exist
        stats.Add(new BuildingStat()
        { 
            statType = mod.modType,
            modifiers = new() { mod }
        });
    }

    /// <summary>
    /// Removes all modifiers from a definition to its neighbours
    /// </summary>
    internal void RemoveModifiersFromDef(Plot plot, BuildingDefinition def)
    {
        foreach (PlotModifier mod in def.startingModifiers)
        {
            foreach (var p in PlotHandler.Instance.GetNeighbours(plot, mod.range))
            {
                p.saveData.modifierModule.RemoveModifier(mod);
            }
        }

    }

    /// <summary>
    /// Removes a modifier to a stat
    /// </summary>
    internal void RemoveModifier(PlotModifier mod)
    {
        isDirty = true;

        foreach (BuildingStat stat in stats)
        {
            if (stat.statType == mod.modType)
            {
                stat.modifiers.Remove(mod);
                return;
            }
        }
    }

    /// <summary>
    /// Calculate all revenue modifiers
    /// </summary>
    private void CalculateRevenueModifiers(BuildingDefinition def)
    {
        stats.Sort((x, y) => x.statType.ToString().CompareTo(y.statType.ToString()));

        foreach (var stat in stats)
        {
            stat.GetRevenueMod(def);
        }

        isDirty = false;
    }

    /// <summary>
    /// Retrieve revenue
    /// </summary>
    internal float GetRevenue(BuildingDefinition def)
    {
        CalculateRevenueModifiers(def);

        float rev = def.income;
        foreach (var stat in stats)
        {
            rev += def.income * stat.revenueMod;
        }
        return rev - def.outcome;
    }

}

[System.Serializable]
public class BuildingStat
{
    [SerializeField] internal BuildingStatType statType;
    [SerializeField] internal float statValue;
    [SerializeField] internal float revenueMod;
    [SerializeField] internal List<PlotModifier> modifiers = new List<PlotModifier>();

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