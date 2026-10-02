using UnityEngine;
using J_Func.UI;
using UnityEngine.UI;

public class BaseMenu : Menu
{
    [SerializeField] private GameObject menuObject;
    [SerializeField] private Button buildModeButton;
    [SerializeField] private Button demolishModeButton;

    public void Init(GameplaySceneController sceneController)
    {
        buildModeButton.onClick.AddListener(
            () => sceneController.EnterBuildMode());
        demolishModeButton.onClick.AddListener(
            () => sceneController.EnterDemolishMode());
    }

    internal override void Open()
    {
        base.Open(); menuObject.SetActive(true);
    }

    internal override void Close()
    {
        base.Close(); menuObject.SetActive(false);
    }
}
