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

        public void OpenAll()
        {
            SetAll(true);
        }
        public void CloseAll()
        {
            SetAll(false);
        }
        public void SetAll(bool state)
        {
            for (int i = 0; i < menuGroup.Length; i++)
            {
                menuGroup[i].SetOpen(state);
            }
        }
    }
}