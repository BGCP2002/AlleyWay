using System;
using UnityEngine;

public class ClickBox : MonoBehaviour, IClickInput
{
    public event Action OnCLick;

    public void OnClick()
    {
        OnCLick?.Invoke();
    }
}
