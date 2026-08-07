using UnityEngine;

[CreateAssetMenu (menuName = "Structure/Group")]
public class StructureGroup : ScriptableObject
{
    [SerializeField] internal StructureDefinition[] definitions;
}
