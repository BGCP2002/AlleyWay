using UnityEditor;
using UnityEngine;

namespace J_Func.UI
{
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] internal MenuGroup menus;
        private void Awake()
        {
            menus.Load(GetComponentsInChildren<Menu>());
        }

        // By Name
        public void ToggleMenu(string menuName)
        {
            menus.ToggleMenu(menuName);
        }
        public void OpenMenu(string menuName)
        {
            menus.SetOpen(menuName, true);
        }
        public void CloseMenu(string menuName)
        {
            menus.SetOpen(menuName, false);
        }

        // All
        public void OpenMenus()
        {
            menus.SetOpen(true);
        }
        public void CloseMenus()
        {
            menus.SetOpen(false);
        }
    }
}