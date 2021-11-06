using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CaracterController : MonoBehaviour {
    
    #region Inspector

    [Header("Dependencies:")]
    [SerializeField] private Rigidbody2D _rb;

    [Header("Movement params:")]
    [SerializeField] private float _acceleration = 50f;
    [SerializeField] private float _groundLinearDrag = 10f;
    [SerializeField] private float _topSpeed = 12f;

    [Header("Jump params:")]
    [SerializeField] private float _jumpingForce = 8f;
    [SerializeField] private float _airLinearDrag = 6f;
    [SerializeField] private float _jumpBuffer = 0.2f;
    [SerializeField] private float _fallGravityMultiplier = 12f;

    [Header("Collision params:")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundRaycastLength = 0.6f;

    #endregion

    private bool _isGrounded => Physics2D.Raycast(transform.position, Vector2.down, _groundRaycastLength, _groundLayer);

    private void Awake() {
        if (_rb != null) return;
        _rb = GetComponent<Rigidbody2D>(); 
    }

    private void Update() {
        _horizontalDirection = GetPlayerMovementInput();
        if (GetPlayerJumpInput()) _currentJumpBuffer = _jumpBuffer;
        _currentJumpBuffer -= Time.deltaTime;
    }
    
    private void FixedUpdate() {
        Move();
        if (_canJump && _shouldJump) Jump();
        ApplyDrag();
        ApplyGravity();
    }

    
    #region Player Input

    private float GetPlayerMovementInput() => Input.GetAxisRaw("Horizontal");
    private bool GetPlayerJumpInput() => Input.GetButtonDown("Jump");
    
    #endregion
    

    #region Movement

    private float _horizontalDirection;
    private bool _isMoving => _horizontalDirection != 0;
    private bool _isStopping => _horizontalDirection == 0;
    private bool _IsChangingDirection => (_movingToRight && _goLeft) || (_movingToLeft && _goRight);
    private bool _movingToLeft => _rb.velocity.x < 0f;
    private bool _movingToRight => _rb.velocity.x > 0f;
    private bool _goLeft => _horizontalDirection < 0f;
    private bool _goRight => _horizontalDirection > 0f;

    private void Move() {
        _rb.AddForce(Vector2.right * _horizontalDirection * _acceleration);
        ClampVelocity();
    }

    private void ClampVelocity() {
        if (Mathf.Abs(_rb.velocity.x) <= _topSpeed) return;
        _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _topSpeed, _rb.velocity.y);
    }

    #endregion


    #region Jump
    
    private float _currentJumpBuffer = 0;
    private bool _shouldJump => _currentJumpBuffer > 0;
    private bool _canJump => _isGrounded;

    private void Jump() => _rb.AddForce(Vector2.up * _jumpingForce, ForceMode2D.Impulse);

    #endregion


    #region Linear Drags & Gravity

    // TODO: The change to the RB drag & gravityScale values should be event driven

    private bool _isFalling => _rb.velocity.y < 0;

    private void ApplyDrag() { 
        if (_isGrounded) ApplyGroundLinearDrag();
        else ApplyAirLinearDrag();
    }
    private void ApplyGravity() => _rb.gravityScale = _isFalling ? _fallGravityMultiplier : 1;
    private void ApplyGroundLinearDrag() => _rb.drag = _isStopping || _IsChangingDirection ? _groundLinearDrag : 0;
    private void ApplyAirLinearDrag() => _rb.drag = _airLinearDrag;

    #endregion


    private void OnDrawGizmos() => Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _groundRaycastLength);

}
