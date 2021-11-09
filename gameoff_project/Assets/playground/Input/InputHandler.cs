using System;
using UnityEngine;

[CreateAssetMenu(menuName = "tmp/input/Input Handler")]
public class InputHandler : ScriptableObject, IInputHandler {

    private enum InputType { CLASSIC, NEW }

    [Header("Settings:")]
    [SerializeField] private InputType _type = InputType.CLASSIC;

    private IInputHandler _input;

    void OnEnable() {
        switch (_type) {
            case InputType.NEW:
                throw new NotImplementedException("A version that use the New Input system is still to be implemented...");
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
