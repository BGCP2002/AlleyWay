using UnityEngine;

[CreateAssetMenu (menuName = "Structure/Definition")]
public class StructureDefinition : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private GameObject prefab;

    [SerializeField] private Vector2Int size = Vector2Int.one;
    [SerializeField] private bool canRotate;
}

public class StructureInstance
{
    [SerializeField] private Vector2Int size = Vector2Int.one;
}
