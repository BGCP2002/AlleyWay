using UnityEngine;

public class GameplayServices : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CameraComponents cameraComponents;
    internal CameraController CameraController { get; private set; }

    [Header("Selection")]
    internal WorldSelection WorldSelection { get; private set; }
    internal WorldRaycaster WorldRaycaster { get; private set; }
    internal BuildSelection BuildSelection { get; private set; }

    [Header("Base State Requirements")]
    [SerializeField] internal BaseMenu BaseMenu;

    [Header("Build State Requirements")]
    [SerializeField] internal StructureMenu StructureMenu;
    [SerializeField] internal StructureSpawner StructureSpawner;
    [SerializeField] internal PlacementPreview PlacementPreview;
    internal PlacementHandler PlacementHandler { get; private set; }
    internal PlacementValidation PlacementValidation { get; private set; }

    public void Init(GameplaySceneController sceneController, GameServices gameServices)
    {
        WorldSelection = new();
        WorldRaycaster = new(gameServices.PlayerInput);
        BuildSelection = new();

        CameraController = new(cameraComponents, gameServices.PlayerInput);

        StructureSpawner.Init(WorldSelection);

        PlacementValidation = new();
        PlacementHandler = new(
            gameServices.PlayerInput,
            StructureSpawner,
            PlacementValidation,
            PlacementPreview,
            BuildSelection);

        BaseMenu.Init(sceneController);
        StructureMenu.Init(sceneController, BuildSelection);
    }
}
