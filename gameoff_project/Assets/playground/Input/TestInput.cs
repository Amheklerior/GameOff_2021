using UnityEngine;

public class TestInput : MonoBehaviour {
   
    [SerializeField] private InputHandler _inputHandler;

    void Update() {
        if(_inputHandler.IsMovementInputGiven) {
            if(_inputHandler.IsRight) Print("RIGHT");
            if(_inputHandler.IsLeft) Print("LEFT");
        } else {
            if (!_inputHandler.IsMovementInputGiven) Print("NO MOVEMENT");
        }
        if (_inputHandler.IsJumpBtnPressed) Print("JUMP");
        if (_inputHandler.IsJumpBtnStillPressed) Print("LONG JUMP");
        Print($"{_inputHandler.PlayerMovementInput}");
    }

    private void Print(string message) => Debug.Log(message);

}
