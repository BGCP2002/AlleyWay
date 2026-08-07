using UnityEngine;
using J_Func.UI;

public class BaseMenu : Menu
{
    protected override void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    protected override void OpenMenu()
    {
        gameObject.SetActive(true);
    }
}
