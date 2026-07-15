using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Header("Position")]
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float focusSpeed;

    [Header("Rotation")]
    [SerializeField] private Transform cameraRotation;
    [SerializeField] private float rotateSpeed;

    [Header("Zoom")]
    [SerializeField] private Transform cameraZoom;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomDistance;
    [SerializeField] private Vector2 zoomRange;

    public enum CameraMode
    { 
        Free,
        Focus,
    }
    private CameraMode mode;
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

        ZoomCamera();
    }

    // Free
    internal void MoveCamera(Vector3 vector)
    {
        mode = CameraMode.Free;

        Vector3 moveVector = cameraPivot.forward * vector.y + cameraPivot.right * vector.x;
        moveVector *= Time.deltaTime * moveSpeed;

        cameraPivot.position += moveVector;
    }    
    internal void RotateCamera(float rotation)
    {
        rotation *= Time.deltaTime * rotateSpeed;

        cameraRotation.eulerAngles = cameraRotation.eulerAngles + new Vector3(0f, rotation);
    }
    internal void ChangeZoom(float zoom)
    {
        zoomDistance = Mathf.Clamp(zoomDistance - zoom * scrollSpeed, zoomRange.x, zoomRange.y);
    }
    internal void ZoomCamera()
    {
        if (Camera.main.orthographic)
        {
            Camera.main.orthographicSize = Mathf.Lerp(
                Camera.main.orthographicSize,
                zoomDistance,
                zoomSpeed * Time.deltaTime
            );
        }
        else
        {
            cameraZoom.localPosition = Vector3.Lerp(
                cameraZoom.localPosition,
                new Vector3(0, 0, -zoomDistance),
                zoomSpeed * Time.deltaTime
            );
        }
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

        cameraPivot.position = Vector3.Lerp(cameraPivot.position, focusModeSettings.target.position, Time.deltaTime * focusSpeed);
    }

}

public class CamFocusModeSettings
{
    internal Transform target;
    internal float moveSpeed;
}