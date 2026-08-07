using System;
using UnityEngine;


namespace J_Func.UI
{
    /// <summary>
    /// Control the state of multiple menus.
    /// </summary>
    [System.Serializable]
    public class MenuGroup
    {
        [SerializeField] private Menu[] menuGroup;
        internal void Load(Menu[] menus)
        {
            menuGroup = menus;
        }

        internal void ToggleMenu(string menuName)
        {
            for (int i = 0; i < menuGroup.Length; i++)
            {
                if (menuGroup[i].menuName == menuName)
                {
                    menuGroup[i].ToggleMenu();
                }
            }
        }
        public void SetOpen(string menuName, bool state)
        {
            for (int i = 0; i < menuGroup.Length; i++)
            {
                if (menuGroup[i].menuName == menuName)
                {
                    menuGroup[i].SetOpen(state);
                }
            }
        }

        public void SetOpen(bool state)
        {
            for (int i = 0; i < menuGroup.Length; i++)
            {
                menuGroup[i].SetOpen(state);
            }
        }
    }
}