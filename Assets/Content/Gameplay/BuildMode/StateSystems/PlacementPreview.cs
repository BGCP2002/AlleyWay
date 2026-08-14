using System;
using UnityEngine;

public class PlacementPreview : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private Transform offset;

    [SerializeField] private GameObject prefab;

    internal void DisablePreview()
    {
        pivot.gameObject.SetActive(false);

        foreach (Transform t in offset)
        {
            Destroy(t.gameObject);
        }
    }

    internal void EnablePreview(StructureDefinition def)
    {
        pivot.gameObject.SetActive(true);

        foreach ( Vector2Int position in def.GetPositions() )
        {
            Instantiate(prefab, new Vector3(position.x, 0, position.y), Quaternion.identity, offset);
        }
    }

    internal void UpdatePreview(PlacementData placementData)
    {
        pivot.transform.position = placementData.MouseWorldPosition;
    }
}
