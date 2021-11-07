using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    
    private Rigidbody2D _rb;
    private InputHandler _input;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>(); 
        _input = new InputHandler();
    }

    private void Update() {
        float _movementInput = _input.GetPlayerMovementInput();
        if (_movementInput > 0) Debug.Log("MOVING TO THE RIGHT");
        else if (_movementInput < 0) Debug.Log("MOVING TO THE LEFT");
        if (_input.IsJumpBtnPressed()) Debug.Log("JUMP");
        if (_input.IsJumpBtnStillPressed()) Debug.Log("LONG JUMP");
    }
}
