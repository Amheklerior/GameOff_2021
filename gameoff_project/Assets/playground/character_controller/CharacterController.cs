using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController {

    private Rigidbody2D _body;

    public CharacterController(
        Rigidbody2D body, 
        float acceleration, 
        float topSpeed, 
        float jumpingForce, 
        float groundLinearDrag, 
        float airLinearDrag,
        float fallGravityMultiplier,
        float lowJumpGravityMultiplier
    ) {
        _body = body;
        _acceleration = acceleration;
        _topSpeed = topSpeed;
        _jumpingForce = jumpingForce;
        _groundDrag = groundLinearDrag;
        _airDrag = airLinearDrag;
        _fallGravityMultiplierl = fallGravityMultiplier;
        _lowJumpGravityMultiplier = lowJumpGravityMultiplier;
    }
    
    #region Movement

    private float _acceleration;
    private float _topSpeed;
    public bool IsMovingToTheRight => _body.velocity.x > 0;
    public bool IsMovingToTheLeft => _body.velocity.x < 0;

    public void Move(Vector2 dir) {
        ApplyMovingForce(dir);
        ClampVelocity();
    }

    private void ApplyMovingForce(Vector2 dir) => _body.AddForce(dir * _acceleration);
    private void ClampVelocity() {
        if (Mathf.Abs(_body.velocity.x) <= _topSpeed) return;
        _body.velocity = new Vector2(Mathf.Sign(_body.velocity.x) * _topSpeed, _body.velocity.y);
    }

    #endregion

    #region Jump

    private float _jumpingForce;
    public bool IsRising => _body.velocity.y > 0;
    public bool IsFalling => _body.velocity.y < 0;

    public void Jump(Vector2 dir) => _body.AddForce(dir * _jumpingForce, ForceMode2D.Impulse);

    #endregion

    #region Drag & Gravity

    private const float DEFAULT_DRAG = 0f;  
    private const float DEFAULT_GRAVITY_MULTIPLIER = 1f;  
    private float _groundDrag;
    private float _airDrag;
    private float _fallGravityMultiplierl;
    private float _lowJumpGravityMultiplier;

    public void ApplyDragAndGravity(PlayerState state, bool isStopping, bool isChangingDirection, bool isLowJump) {
        switch (state) {
            case PlayerState.GROUNDED: 
                _body.drag = isStopping || isChangingDirection ? _groundDrag : DEFAULT_DRAG;
                break;

            case PlayerState.JUMPING:
                _body.gravityScale = isLowJump ? _lowJumpGravityMultiplier : DEFAULT_GRAVITY_MULTIPLIER;
                _body.drag = _airDrag;
                break;

            case PlayerState.FALLING:
                _body.gravityScale = _fallGravityMultiplierl;
                _body.drag = _airDrag;
                break;
        }
    }

    #endregion

}
