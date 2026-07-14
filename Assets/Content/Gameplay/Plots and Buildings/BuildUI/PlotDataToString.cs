using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public static class PlotDataToString
{
    internal static string GetModifierString(Plot plot)
    {
        // Cache modifier module
        PlotModifierModule modModule = plot.saveData.modifierModule;

        // Cache definition if possible
        BuildingDefinition definition = null;
        if (plot.buildingInstance != null)
            definition = plot.buildingInstance.buildingDefinition;

        // Construct modifier information
        string text = string.Empty;
        foreach (var stat in modModule.stats)
        {
            stat.GetRevenueMod(definition);

            switch (stat.revenueMod)
            {
                case 0:
                    text += $"<color=\"black\">";
                    break;
                case < 0:
                    text += $"<color=\"red\">";
                    break;
                case > 0:
                    text += $"<color=\"green\">";
                    break;

            }

            text += $"{stat.statType}: {stat.revenueMod}\n";

            foreach (var mod in stat.modifiers)
            {
                text += $" + {mod.value} {mod.source}\n";
            }
        }

        // Return
        return text;
    }

    internal static string GetRevenueString(Plot plot)
    {
        if (plot.buildingInstance == null) return "";
        float revenue = plot.saveData.modifierModule.GetRevenue(plot.buildingInstance.buildingDefinition);
        return $"Revenue: {revenue}";
    }
    internal static string GetBuildingNameString(Plot plot)
    {
        if (plot.buildingInstance == null) return "Emply Plot";
        return plot.buildingInstance.buildingDefinition.displayName;
    }
}
