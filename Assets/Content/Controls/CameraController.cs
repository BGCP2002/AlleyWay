using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] private Transform cameraPivot;
    public enum CameraMode
    { 
        Free,
        Focus,
    }
    private CameraMode mode;
    [SerializeField] private float moveSpeed;
    private CamFocusModeSettings focusModeSettings;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        switch (mode)
        {
            case CameraMode.Free:
                break;
            case CameraMode.Focus:
                FocusOnTarget();
                break;
        }
    }

    // Free
    internal void MoveCamera(Vector3 vector)
    {
        mode = CameraMode.Free;

        Vector3 moveVector = cameraPivot.forward * vector.y + cameraPivot.right * vector.x;
        moveVector *= Time.deltaTime * moveSpeed;

        cameraPivot.position += moveVector;
    }

    // Focus
    internal void SetFocus(CamFocusModeSettings focusModeSettings)
    {
        this.focusModeSettings = focusModeSettings;
        mode = CameraMode.Focus;
    }
    private void FocusOnTarget()
    {
        if (focusModeSettings == null) { mode = CameraMode.Free; return; }
        if (focusModeSettings.target == null) { mode = CameraMode.Free; return; }

        cameraPivot.position = Vector3.Lerp(cameraPivot.position, focusModeSettings.target.position, Time.deltaTime * moveSpeed);
    }
}

public class CamFocusModeSettings
{
    internal Transform target;
    internal float moveSpeed;
}