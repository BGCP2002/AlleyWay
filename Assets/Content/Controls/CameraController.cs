using System;
using UnityEngine;
using UnityEngine.Windows;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    [SerializeField] private Transform cameraPivot;

    [Header("Position")]
    [SerializeField] private float moveSpeed;

    [Header("Rotation")]
    [SerializeField] private Vector2 pitchRange;
    [SerializeField] private float rotateInputSpeed;
    [SerializeField] private float rotateLerpSpeed;
    private Vector2 targetRotation;

    [Header("Zoom")]
    [SerializeField] private Transform cameraZoom;
    [SerializeField] private Vector2 zoomRange;
    [SerializeField] private float zoomInputSpeed;
    [SerializeField] private float zoomLerpSpeed;
    private float targetZoom;

    [Header("Focus")]
    [SerializeField] private float focusSpeed;
    private Transform focusTarget;

    public enum CameraMode
    { 
        Free,
        Focus,
    }
    private CameraMode mode;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        targetRotation = cameraPivot.eulerAngles;
        targetZoom = Camera.main.orthographicSize; // < - Will need to change with perspective
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

        RotateCamera();
        ZoomCamera();
    }

    // Othrographic Free Mode ==============================================================================
    /// <summary>
    /// Convert input vector to displacement vector and apply it to the camera pivot
    /// </summary>
    internal void MoveCamera(Vector3 inputVector)
    {
        mode = CameraMode.Free;

        Vector3 right = Vector3.ProjectOnPlane(cameraPivot.right, Vector3.up).normalized;
        Vector3 up = Vector3.ProjectOnPlane(cameraPivot.up, Vector3.up).normalized;

        Vector3 moveVector = right * inputVector.x + up * inputVector.y;

        moveVector *= Time.deltaTime * moveSpeed;
        cameraPivot.position += moveVector;
    }

    /// <summary>
    /// Convert input float to rotation and apply it to the camera pivot
    /// </summary>
    internal void ChangeTargetRotation(Vector2 inputVector)
    {
        Vector2 rotation = rotateInputSpeed * Time.deltaTime * new Vector2(inputVector.y, inputVector.x);
        targetRotation += rotation;
        targetRotation.x = Mathf.Clamp(targetRotation.x, pitchRange.x, pitchRange.y);
    }
    internal void RotateCamera()
    {
        cameraPivot.localRotation = Quaternion.Lerp(
            cameraPivot.localRotation,
            Quaternion.Euler(targetRotation),
            rotateLerpSpeed * Time.deltaTime
        );
    }

    /// <summary>
    /// Modify the target distance / orthographic size
    /// </summary>
    internal void ChangeTargetZoom(float zoom)
    {
        targetZoom = Mathf.Clamp(targetZoom - zoom * zoomInputSpeed, zoomRange.x, zoomRange.y);
    }
    /// <summary>
    /// Lerp the current zoom to approach the target zoom over time 
    /// </summary>
    internal void ZoomCamera()
    {
        if (Camera.main.orthographic)
        {
            Camera.main.orthographicSize = Mathf.Lerp(
                Camera.main.orthographicSize,
                targetZoom,
                zoomLerpSpeed * Time.deltaTime
            );
        }
        else
        {
            cameraZoom.localPosition = Vector3.Lerp(
                cameraZoom.localPosition,
                new Vector3(0, 0, -targetZoom),
                zoomLerpSpeed * Time.deltaTime
            );
        }
    }

    // Focus Mode ===========================================================================================
    /// <summary>
    /// Set the focus target
    /// </summary>
    internal void SetFocus(Transform focusTarget)
    {
        this.focusTarget = focusTarget;
        mode = CameraMode.Focus;
    }

    /// <summary>
    /// Lerp the camera to centre on the focus target
    /// </summary>
    private void FocusOnTarget()
    {
        if (focusTarget == null) { mode = CameraMode.Free; return; }
        cameraPivot.position = Vector3.Lerp(cameraPivot.position, focusTarget.position, Time.deltaTime * focusSpeed);
    }

}