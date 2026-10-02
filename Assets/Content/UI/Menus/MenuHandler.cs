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
        public void OpenAll()
        {
            menus.OpenAll();
        }
        public void CloseAll()
        {
            menus.CloseAll();
        }
        public void SetAll(bool state)
        {
            menus.SetAll(state);
        }
    }
}