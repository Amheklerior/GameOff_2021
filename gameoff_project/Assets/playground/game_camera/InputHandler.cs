using UnityEngine;

public interface IInputHandler {
    bool IsLeft { get; }
    bool IsRight { get; }
    bool IsMovementInputGiven { get; }
    float PlayerMovementInput { get; }
    bool IsJumpBtnPressed { get; }
    bool IsJumpBtnStillPressed { get; }
}

[CreateAssetMenu(menuName = "Gameplay/Input Handler")]
public class InputHandler : ScriptableObject, IInputHandler {

    private enum InputType { CLASSIC }

    [Header("Settings:")]
    [SerializeField] private InputType _type = InputType.CLASSIC;

    private IInputHandler _input;

    void OnEnable() {
        switch (_type) {
            case InputType.CLASSIC: 
            default:
                _input = new ClassicInputHandler();
                break;
        }
    }

    public bool IsLeft => _input.IsLeft;
    public bool IsRight => _input.IsRight;
    public bool IsMovementInputGiven => _input.IsMovementInputGiven;
    public float PlayerMovementInput => _input.PlayerMovementInput;
    public bool IsJumpBtnPressed => _input.IsJumpBtnPressed;
    public bool IsJumpBtnStillPressed => _input.IsJumpBtnStillPressed;
    
}

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
