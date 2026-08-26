using UnityEngine;

[CreateAssetMenu(menuName = "Structure/Decor Definition")]
public class DecorDefinition : StructureDefinition
{
    internal override StructureInstance CreateInstance()
    {
        return new DecorInstance();
    }
}

public class DecorInstance : StructureInstance
{

}