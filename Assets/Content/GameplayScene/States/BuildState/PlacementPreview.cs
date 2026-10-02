using System;
using UnityEngine;

public class PlacementPreview : MonoBehaviour
{
    [SerializeField] private Transform pivot;

    [SerializeField] private GameObject zonePrefab;
    [SerializeField] private Transform zoneParent;

    internal void Disable()
    {
        ClearZones();
        pivot.gameObject.SetActive(false);
    }

    internal void SetDefinition(StructureDefinition def)
    {
        pivot.gameObject.SetActive(true);
        ClearZones();
        foreach ( Vector2Int position in def.GetPositions() )
        {
            GameObject zone = Instantiate(zonePrefab, zoneParent);
            zone.transform.localPosition = new Vector3(position.x, 0, position.y);
        }
    }

    private void ClearZones()
    {

        foreach (Transform t in zoneParent)
        {
            Destroy(t.gameObject);
        }
    }

    private void SetColor(Color color)
    {
        foreach (Transform t in zoneParent)
        {
            t.GetComponentInChildren<SpriteRenderer>().color = color;
        }
    }

    internal void UpdatePreview(PlacementData placementData)
    {
        pivot.transform.position = placementData.worldPosition;

        if (placementData.isValid)
            SetColor(Color.white);
        else
            SetColor(Color.red);
    }
}
