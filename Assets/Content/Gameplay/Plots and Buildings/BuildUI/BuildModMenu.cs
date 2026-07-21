using TMPro;
using UnityEngine;

public class BuildModMenu : Menu
{
    [SerializeField] internal TMP_Text displayNameTextBox;
    [SerializeField] internal TMP_Text modifierTextBox;
    [SerializeField] internal TMP_Text revenueTextBox;

    public void OpenModifiers(Plot plot)
    {
        Open();

        displayNameTextBox.text = PlotDataToString.GetBuildingNameString(plot);
        revenueTextBox.text = PlotDataToString.GetRevenueString(plot);
        modifierTextBox.text = PlotDataToString.GetModifierString(plot);
    }

    public override void Open()
    {
        gameObject.SetActive(true);
    }
    public override void Close()
    { 
        gameObject.SetActive(false);

    }
}
