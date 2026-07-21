using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    [Header("Base Menu")]
    [SerializeField] private bool startOpen;
    private void Start()
    {
        if (startOpen)
            Open();
        else
            Close();
    }

    public abstract void Open();
    public abstract void Close();
}