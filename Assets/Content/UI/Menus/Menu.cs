using System.Collections.Generic;
using UnityEngine;

namespace J_Func.UI
{
    /// <summary>
    /// Menu abstract class. Contains Open, Close, Toggle, and Set.
    /// </summary>
    public abstract class Menu : MonoBehaviour
    {
        [Header("Base Menu")]
        [SerializeField] private bool startOpen;
        protected bool isOpen;

        private void Start()
        {
            SetOpen(startOpen);
        }

        internal virtual void Open()
        {
            isOpen = true;
        }

        internal virtual void Close()
        {
            isOpen = false;
        }

        internal virtual void Toggle()
        {
            SetOpen(!isOpen);
        }

        internal virtual void SetOpen(bool state)
        {
            if (state)
                Open();
            else
                Close();
        }
    }
}