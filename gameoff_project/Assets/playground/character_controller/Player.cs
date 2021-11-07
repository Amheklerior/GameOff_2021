using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { GROUNDED, JUMPING, FALLING } 

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

    #region Inspector

    [Header("Movement params:")]
    [SerializeField] private float _acceleration = 70f;
    [SerializeField] private float _topSpeed = 12f;

    [Header("Drag & gravity params:")]
    [SerializeField] private float _groundLinearDrag = 8f;

    #endregion

    #region Dependencies

    private Rigidbody2D _rb;
    private InputHandler _input;
    private CharacterController _controller; 

    #endregion

    #region Player State

    private PlayerState _currentState => PlayerState.GROUNDED;
    private bool _isStopping => !_input.IsMovementInputGiven;
    private bool _isChangingDirection {
        get => (_controller.IsMovingToTheRight && _input.IsLeft) || (_controller.IsMovingToTheLeft && _input.IsRight);
    }

    #endregion

    #region Internals
    private Vector2 _movementDir;
    
    #endregion

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>(); 
        _input = new InputHandler();
        _controller = new CharacterController(
            _rb, 
            _acceleration, 
            _topSpeed, 
            _groundLinearDrag
        );
    }

    private void Update() {
        _movementDir = Vector2.right * _input.PlayerMovementInput;

        // Test jump input
        if (_input.IsJumpBtnPressed) Debug.Log("JUMP");
        if (_input.IsJumpBtnStillPressed) Debug.Log("LONG JUMP");
    }

    private void FixedUpdate() {
        _controller.Move(_movementDir);
        _controller.ApplyDragAndGravity(_currentState, _isStopping, _isChangingDirection);
    }

}

public class CharacterController {

    public CharacterController(Rigidbody2D body, float acceleration, float topSpeed, float groundLinearDrag) {
        _body = body;
        _acceleration = acceleration;
        _topSpeed = topSpeed;
        _groundLinearDrag = groundLinearDrag;
    }
    
    #region Movement

    public bool IsMovingToTheRight => _body.velocity.x > 0;
    public bool IsMovingToTheLeft => _body.velocity.x < 0;

    public void Move(Vector2 dir) {
        ApplyMovingForce(dir);
        ClampVelocity();
    }

    private float _acceleration;
    private float _topSpeed;
    private void ApplyMovingForce(Vector2 dir) => _body.AddForce(dir * _acceleration);
    private void ClampVelocity() {
        if (Mathf.Abs(_body.velocity.x) <= _topSpeed) return;
        _body.velocity = new Vector2(Mathf.Sign(_body.velocity.x) * _topSpeed, _body.velocity.y);
    }

    #endregion

    #region Drag & Gravity

    private const float DEFAULT_DRAG = 0f;  
    private float _groundLinearDrag;
    public void ApplyDragAndGravity(PlayerState state, bool isStopping, bool isChangingDirection) {
        switch (state) {
            case PlayerState.GROUNDED: 
                _body.drag = isStopping || isChangingDirection ? _groundLinearDrag : DEFAULT_DRAG;
                break;
        }
    }

    #endregion

    #region Internals 

    private Rigidbody2D _body;

    #endregion

}
