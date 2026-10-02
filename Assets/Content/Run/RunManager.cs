using UnityEngine;

public class RunManager
{
    public RunManager(GameServices services)
    {
        this.services = services;

        services.SceneTransit.SceneLoaded += InitSceneController;
    }
    private readonly GameServices services;
    private RunContext context;

    public void LoadGame()
    {
        context = new(new RunSaveData()); // -> REPLACE

        services.SceneTransit.LoadScene("GameplayScene");
    }

    public void NewGame()
    {
        context = new(new RunSaveData());

        services.SceneTransit.LoadScene("GameplayScene");
    }

    private void InitSceneController()
    {
        SceneController controller =
            Object.FindFirstObjectByType<SceneController>();

        if (controller == null)
        {
            Debug.LogError("No SceneController found in scene.");
            return;
        }

        controller.Init(services);
        controller.Enter();
    }

    public void Update()
    {
        services.PlayerInput.Update();
    }
}
