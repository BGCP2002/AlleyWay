using System;
using UnityEngine;
using UnityEngine.Windows;

public class CameraController
{
    public CameraController(CameraComponents components, PlayerInput input)
    {
        this.components = components;
        this.input = input;

        cameraPivot = components.cameraPivot;

        targetRotation = cameraPivot.eulerAngles;
        targetZoom = Camera.main.orthographicSize; // < - Will need to change with perspective
    }
    private readonly CameraComponents components;
    private readonly PlayerInput input;

    private readonly Transform cameraPivot;

    private Transform focusTarget;
    private Vector2 targetRotation;
    private float targetZoom;

    public enum CameraMode
    { 
        Free,
        Focus,
    }
    private CameraMode mode;

    public void Update()
    {
        switch (mode)
        {
            case CameraMode.Free:
                MoveCamera();
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
    private void MoveCamera()
    {
        if(input.CameraData.movement.magnitude == 0) { return; }

        Vector2 inputVector = input.CameraData.movement;
        mode = CameraMode.Free;

        Vector3 right = Vector3.ProjectOnPlane(cameraPivot.right, Vector3.up).normalized;
        Vector3 up = Vector3.ProjectOnPlane(cameraPivot.up, Vector3.up).normalized;

        Vector3 moveVector = right * inputVector.x + up * inputVector.y;

        moveVector *= Time.deltaTime * components.moveSpeed;
        cameraPivot.position += moveVector;
    }

    /// <summary>
    /// Slerp to target rotation
    /// </summary>
    private void RotateCamera()
    {
        Vector2 inputVector = input.CameraData.rotation;
        Vector2 rotation = components.rotateInputSpeed * Time.deltaTime * new Vector2(inputVector.y, inputVector.x);
        targetRotation += rotation;
        targetRotation.x = Mathf.Clamp(targetRotation.x, components.pitchRange.x, components.pitchRange.y);

        cameraPivot.localRotation = Quaternion.Lerp(
            cameraPivot.localRotation,
            Quaternion.Euler(targetRotation),
            components.rotateLerpSpeed * Time.deltaTime
        );
    }

    /// <summary>
    /// Lerp the current zoom to approach the target zoom over time 
    /// </summary>
    private void ZoomCamera()
    {
        float zoom = input.CameraData.zoom.y;

        targetZoom = Mathf.Clamp(targetZoom - zoom * components.zoomInputSpeed, components.zoomRange.x, components.zoomRange.y);

        if (Camera.main.orthographic)
        {
            Camera.main.orthographicSize = Mathf.Lerp(
                Camera.main.orthographicSize,
                targetZoom,
                components.zoomLerpSpeed * Time.deltaTime
            );
        }
        else
        {
            components.cameraZoom.localPosition = Vector3.Lerp(
                components.cameraZoom.localPosition,
                new Vector3(0, 0, -targetZoom),
                components.zoomLerpSpeed * Time.deltaTime
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
        cameraPivot.position = Vector3.Lerp(cameraPivot.position, focusTarget.position, Time.deltaTime * components.focusSpeed);
    }
}