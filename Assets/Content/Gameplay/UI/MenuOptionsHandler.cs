using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuOptionsHandler : MonoBehaviour
{
    [SerializeField] private GameObject menuOptionPrefab;
    [SerializeField] private Transform menuOptionParent;

    internal void ClearOptions()
    {
        foreach (Transform t in menuOptionParent)
        {
            Destroy(t.gameObject);
        }
    }

    internal void AddMenuOption(MenuOption optionData)
    {
        GameObject newMenuOption = Instantiate(menuOptionPrefab, menuOptionParent);

        // Button
        Button button = newMenuOption.GetComponentInChildren<Button>();
        button.onClick.AddListener(optionData.onClickAction);

        // Text
        TMP_Text textBox = newMenuOption.GetComponentInChildren<TMP_Text>();
        textBox.text = optionData.displayText;
    }
}
public class MenuOption
{
    internal string displayText = "";
    internal UnityAction onClickAction;
}
