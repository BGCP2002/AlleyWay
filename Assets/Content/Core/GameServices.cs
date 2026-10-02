using UnityEngine;

public class GameServices : MonoBehaviour
{
    internal RunManager RunManager { get; private set; }
    internal PlayerInput PlayerInput { get; private set; }
    internal SceneTransit SceneTransit { get; private set; }

    public void Init()
    {
        PlayerInput = new();
        SceneTransit = new();
        RunManager = new(this);
    }
}
