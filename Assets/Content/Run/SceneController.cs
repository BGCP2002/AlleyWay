using UnityEngine;

public abstract class SceneController : MonoBehaviour
{
    protected GameServices Services { get; private set; }

    public void Init(GameServices services)
    {
        Services = services;
        OnInit();
    }

    public void Enter()
    {
        OnEnter();
    }

    public void Exit()
    {
        OnExit();
    }

    protected virtual void OnInit() { }
    protected abstract void OnEnter();
    protected abstract void OnExit();
}