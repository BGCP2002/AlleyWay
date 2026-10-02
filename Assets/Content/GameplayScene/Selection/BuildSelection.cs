using System;

public class BuildSelection
{
    public event Action<StructureDefinition> StructureSelected;
    public StructureDefinition SelectedStructure { get; private set; }
    public void SelectStructure(StructureDefinition definition)
    {
        SelectedStructure = definition;
        StructureSelected?.Invoke(definition);
    }
}
