using UnityEngine;

public class ClickBox : MonoBehaviour, IClick
{
    private IClick clickable;

    private void Awake()
    {
        clickable = transform.parent.GetComponentInParent<IClick>();
    }

    public void OnClick()
    {
        clickable?.OnClick();
    }
}
