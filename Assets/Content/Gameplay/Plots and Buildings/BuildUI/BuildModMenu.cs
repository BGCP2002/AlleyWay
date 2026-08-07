using TMPro;
using UnityEngine;
using J_Func.UI;

public class BuildModMenu : Menu
{
    [SerializeField] internal TMP_Text displayNameTextBox;
    [SerializeField] internal TMP_Text modifierTextBox;
    [SerializeField] internal TMP_Text revenueTextBox;

    public void OpenModifiers(Plot plot)
    {
        OpenMenu();

        displayNameTextBox.text = PlotDataToString.GetBuildingNameString(plot);
        revenueTextBox.text = PlotDataToString.GetRevenueString(plot);
        modifierTextBox.text = PlotDataToString.GetModifierString(plot);
    }

    protected override void OpenMenu()
    {
        gameObject.SetActive(true);
    }
    protected override void CloseMenu()
    { 
        gameObject.SetActive(false);

    }
}
