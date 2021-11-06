using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CaracterController : MonoBehaviour {
    
    #region Inspector

    [Header("Dependencies:")]
    [SerializeField] private Rigidbody2D _rb;

    [Header("Movement params:")]
    [SerializeField] private float _acceleration;
    [SerializeField] private float _linearDrag;
    [SerializeField] private float _topSpeed;

    #endregion


    private void Awake() {
        if (_rb != null) return;
        _rb = GetComponent<Rigidbody2D>(); 
    }

    private void Update() => _horizontalDirection = GetPlayerMovementInput();
    private void FixedUpdate() => Move();

    
    #region Player Input

    private float GetPlayerMovementInput() => Input.GetAxisRaw("Horizontal");
    
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
        ApplyDrag();
    }

    private void ClampVelocity() {
        if (Mathf.Abs(_rb.velocity.x) <= _topSpeed) return;
        _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _topSpeed, _rb.velocity.y);
    }

    private void ApplyDrag() => _rb.drag = _isStopping || _IsChangingDirection ? _linearDrag : 0;

    #endregion

}
