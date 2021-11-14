using System;
using UnityEngine;
using Cinemachine;

namespace gameoffjam {
    public class CamController : MonoBehaviour {
        
        #region Inspector interface 

        [Header("Dependencies:")]
        [SerializeField] private CinemachineVirtualCamera _cam;
        [SerializeField] private InputHandler _inputHandler;

        [Header("Settings:")]
        [SerializeField][Range(0f, 0.5f)] private float _framingAnticipation = 0.25f;
        [SerializeField][Range(0f, 1f)] private float _reframingTime = 0.1f;
        [SerializeField][Range(0f, 5f)] private float _reframeToCenterDelay = 3f;

        #endregion

        void Start() => Init();

        void Update() {
            if (IsPlayerMoving) {
                if (HasNoDeadzone) SetDefaultDeadzone();
                if (!IsCameraMoving) return;
                if (!IsTargetFramingProperlySet) {
                    SetProperFramingTarget();
                    ResetReframingToCenterDelay();
                }
                if (!IsCurrentShotCloseEnough) ReframeShot();
                UpdateTransformPosition();
            } else {
                if (IsFrameCentered) return;
                if (!HasCenteringDelayPassed) {
                    ComputeReframingToCenterDelay();
                    return;
                }
                if (HasDeadzone) SetNoDeadzone();
                ReframeShotToCenter();
            }
        }
        

        #region Internals

        private CinemachineFramingTransposer _transposer;
        private float _targetScreenX;
        private float _screenCenterX = 0.5f;

        private void Init() {
            if (!_cam) _cam = GetComponent<CinemachineVirtualCamera>();
            if (!_cam) throw new NullReferenceException("PlayerCam: No virtual camera component has been found!");
            if (!_inputHandler) throw new NullReferenceException("PlayerCam: No input handler has been provided!");
            _transposer = _cam.GetCinemachineComponent<CinemachineFramingTransposer>();
            _targetScreenX = _transposer.m_ScreenX;
            _defaultDeadZoneWidth = _transposer.m_DeadZoneWidth;
            _prevXPosition = transform.position.x;
        }

        #region Player Input Monitoring

        private bool IsPlayerMoving => _inputHandler.IsMovementInputGiven;
        private bool MovingToTheRight => _inputHandler.IsRight;
        private bool MovingToTheLeft => _inputHandler.IsLeft; 

        #endregion

        #region Deadzone management

        private float _defaultDeadZoneWidth;
        private bool HasDeadzone => _transposer.m_DeadZoneWidth > 0;
        private bool HasNoDeadzone => !HasDeadzone;
        private void SetNoDeadzone() => _transposer.m_DeadZoneWidth = 0;
        private void SetDefaultDeadzone() => _transposer.m_DeadZoneWidth = _defaultDeadZoneWidth;

        #endregion

        #region Action framing management

        private float _prevXPosition;
        private float _velocity = 0.0f;
        private bool IsCameraMoving => IsCameraMovingToTheRight || IsCameraMovingToTheLeft;
        private bool IsCameraMovingToTheRight => transform.position.x > _prevXPosition;
        private bool IsCameraMovingToTheLeft => transform.position.x < _prevXPosition;
        private bool IsCurrentShotCloseEnough => Mathf.Approximately(_transposer.m_ScreenX, _targetScreenX);
        private bool IsTargetFramingProperlySet => (MovingToTheRight && _targetScreenX == _screenCenterX - _framingAnticipation) 
            || (MovingToTheLeft && _targetScreenX == _screenCenterX + _framingAnticipation);

        private void SetProperFramingTarget() {
            _targetScreenX = MovingToTheRight 
                ?  _screenCenterX - _framingAnticipation 
                : _screenCenterX + _framingAnticipation;
        }

        private void ReframeShot() {
            _transposer.m_ScreenX = Mathf.SmoothDamp(_transposer.m_ScreenX, _targetScreenX, ref _velocity, _reframingTime);
        }

        private void UpdateTransformPosition() => _prevXPosition = transform.position.x;

        #endregion

        #region Stationary framing management

        private float _reframeToCenterCounter = 0f;

        private bool IsFrameCentered => Mathf.Approximately(_transposer.m_ScreenX, _screenCenterX);
        private bool HasCenteringDelayPassed => _reframeToCenterCounter < 0f;
        private void ComputeReframingToCenterDelay() => _reframeToCenterCounter -= Time.deltaTime;
        private void ResetReframingToCenterDelay() => _reframeToCenterCounter = _reframeToCenterDelay;
        private void ReframeShotToCenter() {
            _transposer.m_ScreenX = Mathf.SmoothDamp(_transposer.m_ScreenX, _screenCenterX, ref _velocity, _reframingTime);
        }

        #endregion

        #endregion
    }
}
