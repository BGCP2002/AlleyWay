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
        [SerializeField] internal string menuName;
        [SerializeField] private bool isOpen;

        private void Start()
        {
            SetOpen(isOpen);
        }

        protected abstract void OpenMenu();
        protected abstract void CloseMenu();
        public virtual void ToggleMenu()
        {
            if (isOpen)
            {
                isOpen = false;
                CloseMenu();
            }
            else
            {
                isOpen = true;
                OpenMenu();
            }
        }
        public virtual void SetOpen(bool state)
        {
            if (state)
            {
                isOpen = true;
                OpenMenu();
            }
            else
            {
                isOpen = false;
                CloseMenu();
            }
        }
    }
}