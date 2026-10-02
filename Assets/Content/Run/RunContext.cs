using System.Collections.Generic;
using UnityEngine;

public class RunContext
{
    public RunContext(RunSaveData saveData)
    {
        SaveData = saveData;
    }

    public RunSaveData SaveData { get; set; }
}

public class RunSaveData
{
    public Dictionary<Vector2Int, StructureSaveData> structureData { get; private set; } = new();

    public void AddStructure()
    {

    }
    public void RemoveStructure()
    {

    }
}

public class StructureSaveData
{
    // EX:
    // level
    // moneyEarntTotal
}
