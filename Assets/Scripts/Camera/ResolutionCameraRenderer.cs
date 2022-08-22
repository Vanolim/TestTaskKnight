using UnityEngine;

public class ResolutionCameraRenderer : MonoBehaviour
{
    [SerializeField] private Vector2 _defaultResolution;
    [SerializeField][Range(0f, 1f)] private float _widthOrHeight;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        Init();
    }

    private void Init()
    {
        float initialSize = _camera.orthographicSize;
        float targetAspect = _defaultResolution.x / _defaultResolution.y;

        SetCameraSize(initialSize, targetAspect);
    }

    private void SetCameraSize(float initialSize, float targetAspect)
    {
        float constantWidthSize = initialSize * (targetAspect / _camera.aspect);
        _camera.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, _widthOrHeight);
    }
} 