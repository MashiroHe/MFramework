using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _yaw;
    [SerializeField] private Transform _pitch;
    [SerializeField] private float _pitchMin = -85;
    [SerializeField] private float _pitchMax = 85;
    [SerializeField] private float _factor = 0.4f;
        
    [SerializeField] private float _defaultPitch = 45;
    [SerializeField] private float _defaultYaw = 45;

    private float _currentPitch;
    private float _currentYaw;
    
    private Vector2 _previousPosition;
    private bool _dragging = false;
    
    private void Start()
    {
        ResetPitchAndYaw();
        
        ProcessTouchMove(Vector3.zero, Vector3.zero);
    }

    private void ResetPitchAndYaw()
    {
        _currentPitch = _defaultPitch;
        _currentYaw = _defaultYaw;
    }

    public void SetTarget(Transform target)
    {
        var transform = gameObject.transform;
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
    }
    
    private void ProcessTouchMove(Vector2 previous, Vector2 current)
    {
        if (Application.isPlaying)
        {
            var offset = current - previous;

            offset *= _factor;
        
            var xOffset = offset.x;
            var yOffset = offset.y;

            _currentYaw += xOffset;
            _currentYaw = _currentYaw % 360;
            _yaw.localRotation = Quaternion.AngleAxis(_currentYaw, Vector3.up);

            _currentPitch -= yOffset;
            _currentPitch = Mathf.Clamp(_currentPitch, _pitchMin, _pitchMax);
        
            _pitch.localRotation = Quaternion.AngleAxis(_currentPitch, Vector3.right); 
        }
        else
        {
            _yaw.localRotation = Quaternion.AngleAxis(_defaultYaw, Vector3.up);
            _pitch.localRotation = Quaternion.AngleAxis(_defaultPitch, Vector3.right);
        }
    }
    
    private void Update()
    {
        if (Application.isPlaying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _dragging = true;
                _previousPosition = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _dragging = false;
            }

            if (_dragging)
            {
                ProcessTouchMove(_previousPosition, Input.mousePosition);
                _previousPosition = Input.mousePosition;
            }
        }
        else
        {
            ProcessTouchMove(Vector2.zero, Vector2.zero);
        }
    }
}
