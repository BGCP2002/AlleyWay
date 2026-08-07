using System;
using UnityEngine;

public class PlacementPreview : MonoBehaviour
{
    [SerializeField] private MeshFilter placementGuide;

    internal void DisablePreview()
    {
        placementGuide.gameObject.SetActive(false);
    }

    internal void EnablePreview(StructureDefinition def)
    {
        placementGuide.gameObject.SetActive(true);

        placementGuide.transform.localScale = new Vector3(def.size.x, 0, def.size.y);
    }

    internal void UpdatePreview(PlacementData placementData)
    {
        placementGuide.transform.position = placementData.WorldPivot;
    }
}
