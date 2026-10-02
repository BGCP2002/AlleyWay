using UnityEngine;

[System.Serializable]
public class StructureAreaModifier
{
    [SerializeField] internal BuildingStatType modType;
    [SerializeField] internal float value = 0;
    [SerializeField] internal int range = 0;
    [SerializeField] internal string source = "";
}

public enum BuildingStatType
{
    Noise,
    Nightlife,
    Tourist,
    Youth,
    Adult,
    Luxury,
    Cleanliness,
}