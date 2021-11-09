using System;
using UnityEngine;

public class FollowAhead : MonoBehaviour {

    [Header("Dependencies:")]
    [SerializeField] private MovementDirection _direction;
    [SerializeField] private Transform _target;

    [Header("Settings:")]
    [SerializeField] private Vector2 _leadingOffset = DEFAULT_LEADING_OFFSET;

    private static Vector2 DEFAULT_LEADING_OFFSET = new Vector3(6f, 1.5f);

    private Vector3 _offset = DEFAULT_LEADING_OFFSET;

    private void Start() {
        if (_target == null) throw new NullReferenceException("Follow Ahead: No target to follow has been provided!");
    }

    private void FixedUpdate() {
        switch (_direction.Value) {
            case Direction.RIGHT:
                _offset = new Vector3(_leadingOffset.x, _leadingOffset.y, 0f);
                transform.position = _target.position + _offset;
                break;
            case Direction.LEFT:
                _offset = new Vector3(-_leadingOffset.x, _leadingOffset.y, 0f);
                transform.position = _target.position + _offset;
                break;
            default:
                _offset = _direction.PreviousValue == Direction.RIGHT
                    ? new Vector3(_leadingOffset.x, _leadingOffset.y, 0f)
                    : new Vector3(-_leadingOffset.x, _leadingOffset.y, 0f);
                transform.position = Vector3.Lerp(transform.position, _target.position + _offset, Time.deltaTime);
                break;
        }
    }
    
}
