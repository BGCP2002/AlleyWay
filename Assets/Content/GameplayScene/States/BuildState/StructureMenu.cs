using UnityEngine;
using UnityEngine.UI;
using J_Func.UI;
using System;

public class StructureMenu : Menu
{
    [Header("References")]
    [SerializeField] private GameObject menuObject;

    [Header("Tabs")]
    [SerializeField] private Button[] tabs;
    private int currentTabIndex = -1;

    [Header("Structure Selection")]
    [SerializeField] private StructureGroup[] structureGroups;
    [SerializeField] private StructureButton structureButtonPrefab;
    [SerializeField] private Transform structureParent;

    internal void Init(GameplaySceneController sceneController, BuildSelection buildSelection)
    {
        this.buildSelection = buildSelection;

        InitTabs();
    }
    private BuildSelection buildSelection;

    // Menu State ======================================================================================

    internal override void Open()
    {
        base.Open(); menuObject.SetActive(true);

        // Load
        LoadTab(currentTabIndex);
    }

    internal override void Close()
    {
        base.Close(); menuObject.SetActive(false);
    }

    // Structure Option Display ======================================================================================

    public void LoadTab(int index)
    {
        index = Mathf.Clamp(index, 0, tabs.Length);
        if (index != currentTabIndex)
        {
            currentTabIndex = index;
            LoadStructures();
        }
    }
    private void LoadStructures()
    {
        ClearStructureButtons();
        foreach (StructureDefinition def in structureGroups[currentTabIndex].definitions)
        {
            AddStructureButton(def);
        }
    }
    private void AddStructureButton(StructureDefinition def)
    {
        StructureButton button = Instantiate(structureButtonPrefab, structureParent);
        button.Init(def);
        button.GetComponent<Button>()
            .onClick.AddListener(() => buildSelection.SelectStructure(def));
    }
    private void ClearStructureButtons()
    {
        foreach (Transform t in structureParent)
        {
            t.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(t.gameObject);
        }
    }

    // Tabs
    private void InitTabs()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            int index = i; // By Val
            tabs[i].onClick.AddListener(
               () => LoadTab(index)
               );
        }
    }
}
