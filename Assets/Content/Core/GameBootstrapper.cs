using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    public static GameBootstrapper Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Init();
    }

    [SerializeField] private GameServices GameServices;

    private void Init()
    {
        GameServices.Init();
    }

    private void Update()
    {
        GameServices.RunManager.Update();
    }
}
