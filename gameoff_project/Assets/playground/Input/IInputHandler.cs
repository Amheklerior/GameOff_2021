using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler {
    
    bool IsLeft { get; }
    bool IsRight { get; }
    bool IsMovementInputGiven { get; }
    float PlayerMovementInput { get; }
    bool IsJumpBtnPressed { get; }
    bool IsJumpBtnStillPressed { get; }

}
