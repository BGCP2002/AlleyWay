using UnityEngine;

public class Building : MonoBehaviour, IClick
{
    public void OnClick()
    {
        CameraController.Instance.SetFocus(transform);
    }
}
