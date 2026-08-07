using TMPro;
using UnityEngine;
using UnityEngine.UI;
using J_Func.UI;
using System;

public class StructureMenu : Menu
{
    [Header("Structure Menu")]
    [SerializeField] private StructureGroup[] structureGroups;
    private bool isDirty = true;

    [Header("Structure Selection")]
    [SerializeField] private GameObject structureUI;
    [SerializeField] private Transform structureParent;
    public event Action<StructureDefinition> OnStructureSelected;

    // Menu State ======================================================================================
    protected override void CloseMenu()
    {
        gameObject.SetActive(false);
    }
    protected override void OpenMenu()
    {
        gameObject.SetActive(true);
        Display();
    }


    // Structure Option Display ======================================================================================
    private void ClearDisplay()
    {
        foreach (Transform t in structureParent)
        {
            Destroy(t.gameObject);
        }
    }
    private void Display()
    {
        if (isDirty)
        {
            isDirty = false;
            ClearDisplay();
            foreach (StructureDefinition def in structureGroups[0].definitions)
            {
                DisplayStructure(def);
            }
        }
    }
    private void DisplayStructure(StructureDefinition def)
    {
        GameObject button = Instantiate(structureUI, structureParent);

        button.GetComponentInChildren<TMP_Text>().text = def.displayName;

        button.GetComponent<Button>()
            .onClick.AddListener(() => SelectStructure(def));
    }

    // Structure Option Selection ======================================================================================
    private void SelectStructure(StructureDefinition def)
    {
        OnStructureSelected?.Invoke(def);
    }
}
