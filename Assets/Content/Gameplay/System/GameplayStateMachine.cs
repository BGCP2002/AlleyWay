using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class GameplayStateMachine : MonoBehaviour, IMouseInput, IClickInput, IRotationInput
{
    public enum GameplayState
    {
        DioramaMode,
        BuildMode,
        FreeCameraMode,
    }

    internal GameplayState state { get; private set; }

    [SerializeField] private GameState baseState;
    [SerializeField] private BuildStateManager buildState;
    private Dictionary<GameplayState, GameState> gameStateDict = new();

    private void Awake()
    {
        gameStateDict.Add(GameplayState.DioramaMode, baseState);
        gameStateDict.Add(GameplayState.BuildMode, buildState);
    }

    public void SetState(GameplayState state)
    {
        gameStateDict[this.state]?.OnExit();
        this.state = state;
        gameStateDict[this.state]?.OnEnter();
    }

    // Button Input =====================================================
    public void EnterBuildMode()
    {
        SetState(GameplayState.BuildMode);
    }

    // Input Passing =====================================================
    public void UpdateMouseInfo(MouseInfo info)
    {
        if (gameStateDict[state] is IMouseInput mouseState)
            mouseState.UpdateMouseInfo(info);
    }

    public void LeftClick(MouseInfo click)
    {
        Debug.Log("Clicked!");
        if (gameStateDict[state] is IClickInput clickState)
            clickState.LeftClick(click);
        else
            Debug.Log("No IClickInput!");
    }

    public void RightClick(MouseInfo click)
    {
        if (gameStateDict[state] is IClickInput clickState)
            clickState.RightClick(click);
    }

    public void Rotation(float dir)
    {
        if (gameStateDict[state] is IRotationInput rotationState)
            rotationState.Rotation(dir);
    }

}
