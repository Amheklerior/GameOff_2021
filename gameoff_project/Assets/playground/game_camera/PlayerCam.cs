using System;
using UnityEngine;
using Cinemachine;

public class PlayerCam : MonoBehaviour {
    
    [Header("Dependencies:")]
    [SerializeField] private CinemachineVirtualCamera _cam;

    [Header("Settings:")]
    [SerializeField][Range(0f, 0.5f)] private float _framingAnticipation = 0.2f;
    [SerializeField][Range(0f, 1f)] private float _reframingTime = 0.5f;


    void Start() => Init();

    void Update() {
        if (!IsMoving) return;
        if (!IsTargetFramingProperlySet) SetProperTargetFraming();  
        if(!IsCurrentShotCloseEnough) ReframeShot();
        // else Reset_velocity = 0.0f;
        UpdateTransformPosition();
    }


    #region Internals

    private CinemachineFramingTransposer _transposer;

    private float _prevXPosition;
    private float _targetScreenX;
    private float _screenCenterX = 0.5f;
    private float _velocity = 0.0f;

    private bool IsMoving => MovingToTheRight || MovingToTheLeft;
    private bool MovingToTheRight => transform.position.x > _prevXPosition;
    private bool MovingToTheLeft => transform.position.x < _prevXPosition;
    private bool IsCurrentShotCloseEnough => Mathf.Approximately(_transposer.m_ScreenX, _targetScreenX);
    private bool IsTargetFramingProperlySet => (MovingToTheRight && _targetScreenX == _screenCenterX - _framingAnticipation) 
        || (MovingToTheLeft && _targetScreenX == _screenCenterX + _framingAnticipation);

    private void Init() {
        if (!_cam) _cam = GetComponent<CinemachineVirtualCamera>();
        if (!_cam) throw new NullReferenceException("PlayerCam: No virtual camera component has been found!");
        _transposer = _cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        _targetScreenX = _transposer.m_ScreenX;
        _prevXPosition = transform.position.x;
    }

    private void ReframeShot() {
        _transposer.m_ScreenX = Mathf.SmoothDamp(_transposer.m_ScreenX, _targetScreenX, ref _velocity, _reframingTime);
    }

    private void SetProperTargetFraming() {
        _targetScreenX = MovingToTheRight 
            ?  _screenCenterX - _framingAnticipation 
            : _screenCenterX + _framingAnticipation;
    }

    private void UpdateTransformPosition() => _prevXPosition = transform.position.x;

    #endregion

}
