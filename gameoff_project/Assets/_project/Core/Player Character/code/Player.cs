using System;
using UnityEngine;

namespace gameoffjam {

    public enum PlayerState { GROUNDED, JUMPING, FALLING } 

    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour {

        #region Inspector

        [Header("Dependencies:")]
        [SerializeField] private InputHandler _input;
        
        [Header("Movement params:")]
        [SerializeField] private float _acceleration = 70f;
        [SerializeField] private float _topSpeed = 12f;

        [Header("Jump params:")]
        [SerializeField] private float _jumpingForce = 10f;
        [SerializeField] private float _jumpBufferLength = 0.2f;

        [Header("Drag & gravity params:")]
        [SerializeField] private float _groundDrag = 8f;
        [SerializeField] private float _airDrag = 0.5f;
        [SerializeField] private float _fallGravityMultiplier = 8f;
        [SerializeField] private float _lowJumpGravityMultiplier = 5f;

        [Header("Collision params:")]
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundRaycastLength = 0.6f;

        [Header("Audio clips:")]
        [SerializeField] private AudioClip _jumpSoundClip;
        [SerializeField] private AudioClip _landSoundClip;
        [SerializeField] private AudioClip _moveSoundClip;

        #endregion

        #region Component Lifecycle

        private void Awake() => SetupDependencies();
        private void Start() => Init();
        private void Update() => StorePlayerInputs();
        private void FixedUpdate() {
            if (_shouldJump) _controller.Jump(Vector2.up);
            if (_changedDirection) FlipSprite();
            _controller.Move(_movementDir);
            _controller.ApplyDragAndGravity(_currentState, _isStopping, _isChangingDirection, _isLowJump, _input.IsMovementInputGiven);
            UpdateAnimController();
        }
        private void OnDestroy() => TearDown();
        private void OnDrawGizmos() => DrawGroundRayChecker();

        #endregion

        #region Player State

        private PlayerState _currentState {
            get {
                if (_isGrounded) return PlayerState.GROUNDED;
                return _controller.IsFalling ? PlayerState.FALLING : PlayerState.JUMPING;
            }
        }

        private bool _isGrounded => Physics2D.Raycast(transform.position, Vector2.down, _groundRaycastLength, _groundLayer);
        private bool _isStopping => !_input.IsMovementInputGiven;
        private bool _canJump => _isGrounded;
        private bool _isChangingDirection {
            get => (_controller.IsMovingToTheRight && _input.IsLeft) || (_controller.IsMovingToTheLeft && _input.IsRight);
        }

        #endregion

        #region Internals

        private Rigidbody2D _rb;
        private CharacterController _controller; 
        private Animator _animator;
        private AudioSource _audioPlayer;
        private PlayerAnimationController _animController;
        private SpriteRenderer _sprite;
        private Vector2 _movementDir;
        private float _jumpBuffer = 0f;
        private bool _shouldJump => _canJump && _IsJumpInputBuffered;
        private bool _IsJumpInputBuffered => _jumpBuffer > 0;
        private bool _isLowJump => !_input.IsJumpBtnStillPressed;
        private bool _isFacingRight => !_sprite.flipX;
        private bool _isFacingLeft => _sprite.flipX;
        private bool _changedDirection {
            get => _input.IsMovementInputGiven && (
                _controller.IsMovingToTheRight && _isFacingLeft || 
                _controller.IsMovingToTheLeft && _isFacingRight
            );
        }

        private void SetupDependencies() {
            _rb = GetComponent<Rigidbody2D>();
            _sprite = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _audioPlayer = GetComponent<AudioSource>();

            if (!_rb) 
                throw new NullReferenceException("Player: No Rigidbody2D component has been attached to the player object!");
            if (!_sprite) 
                throw new NullReferenceException("Player: No SpriteRenderer component has been attached to the player object!");
            if (!_animator) 
                throw new NullReferenceException("Player: No Animator component has been attached to the player object!");
            if (!_audioPlayer) 
                throw new NullReferenceException("Player: No AudioSource component has been attached to the player object!");
            if (!_input) 
                throw new NullReferenceException("Player: No ref to the input handler has been found!");
        }

        private void Init() {
            _controller = new CharacterController(
                _rb, 
                _acceleration, 
                _topSpeed, 
                _jumpingForce,
                _groundDrag,
                _airDrag,
                _fallGravityMultiplier,
                _lowJumpGravityMultiplier
            );
            _animController = new PlayerAnimationController(_animator);
        }

        private void TearDown() {
            _rb = null;
            _controller = null;
        }
        
        private void StorePlayerInputs() {
            _movementDir = Vector2.right * _input.PlayerMovementInput;
            if (_input.IsJumpBtnPressed) {
                _animController.StartJumpAnimation();
                _jumpBuffer = _jumpBufferLength;
            }
            else _jumpBuffer -= Time.deltaTime;
        }

        private void UpdateAnimController() { 
            _animController.UpdateGroundedCheck(_isGrounded);
            _animController.UpdateMovingSpeed(Mathf.Abs(_rb.velocity.x));
            _animController.UpdateJumpingSpeed(_rb.velocity.y);
        }

        private void FlipSprite() => _sprite.flipX = !_sprite.flipX;

        private void DrawGroundRayChecker() { 
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _groundRaycastLength);
        }

        #endregion

        #region Audio Management

        private void PlayJumpSound() {
            _audioPlayer.clip = _jumpSoundClip;
            _audioPlayer.loop = false;
            _audioPlayer.Play();
        }

        private void PlayLandingSound() {
            _audioPlayer.clip = _landSoundClip;
            _audioPlayer.loop = false;
            _audioPlayer.Play();
        }
        
        private void PlayMovementSound() {
            if (_audioPlayer.clip == _moveSoundClip && _audioPlayer.isPlaying) return;
            _audioPlayer.clip = _moveSoundClip;
            _audioPlayer.loop = true;
            _audioPlayer.Play();
        }

        private void StopAudio() {
            _audioPlayer.Stop();
            _audioPlayer.clip = null;
        }

        #endregion

    }
}
