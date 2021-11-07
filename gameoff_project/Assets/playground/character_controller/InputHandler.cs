using UnityEngine;

public sealed class InputHandler {

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

    public InputHandler(InputMap _map) => _inputMap = _map;
    public InputHandler() : this(DEFAULT_MAP) {}
    
    public float GetPlayerMovementInput() => Input.GetAxisRaw(_inputMap.MovementAxisId);
    public bool IsJumpBtnPressed() => Input.GetButtonDown(_inputMap.JumpButtonId);
    public bool IsJumpBtnStillPressed() => Input.GetButton(_inputMap.JumpButtonId);
    
}
