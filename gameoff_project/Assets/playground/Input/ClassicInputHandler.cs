using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ClassicInputHandler : IInputHandler {

    public static InputMap DEFAULT_MAP = new InputMap("Horizontal", "Jump");

    public struct InputMap {

        public InputMap(string movementAxisId, string jumpButtonId) {
            MovementAxisId = movementAxisId;
            JumpButtonId = jumpButtonId;
        }

        public string MovementAxisId { get; }
        public string JumpButtonId { get; }
    }

    private InputMap _inputMap;

    public ClassicInputHandler(InputMap _map) => _inputMap = _map;
    public ClassicInputHandler() : this(DEFAULT_MAP) {}

    public bool IsLeft => Input.GetAxisRaw(_inputMap.MovementAxisId) < 0;
    public bool IsRight => Input.GetAxisRaw(_inputMap.MovementAxisId) > 0;
    public bool IsMovementInputGiven => Input.GetAxisRaw(_inputMap.MovementAxisId) != 0;
    public float PlayerMovementInput => Input.GetAxisRaw(_inputMap.MovementAxisId);
    public bool IsJumpBtnPressed => Input.GetButtonDown(_inputMap.JumpButtonId);
    public bool IsJumpBtnStillPressed => Input.GetButton(_inputMap.JumpButtonId);
    
}
