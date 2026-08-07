using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    internal abstract void OnEnter();
    internal abstract void OnExit();
}



