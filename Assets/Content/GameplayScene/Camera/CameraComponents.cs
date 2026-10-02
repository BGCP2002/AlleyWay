using UnityEngine;

public class CameraComponents : MonoBehaviour
{
    [Header("Pivots")]
    [SerializeField] internal Transform cameraPivot;

    [Header("Position")]
    [SerializeField] internal float moveSpeed;

    [Header("Rotation")]
    [SerializeField] internal Vector2 pitchRange;
    [SerializeField] internal float rotateInputSpeed;
    [SerializeField] internal float rotateLerpSpeed;

    [Header("Zoom")]
    [SerializeField] internal Transform cameraZoom;
    [SerializeField] internal Vector2 zoomRange;
    [SerializeField] internal float zoomInputSpeed;
    [SerializeField] internal float zoomLerpSpeed;

    [Header("Focus")]
    [SerializeField] internal float focusSpeed;
}
