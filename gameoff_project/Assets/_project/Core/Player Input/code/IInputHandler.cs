
namespace gameoffjam {

    public interface IInputHandler {
        bool IsLeft { get; }
        bool IsRight { get; }
        bool IsMovementInputGiven { get; }
        float PlayerMovementInput { get; }
        bool IsJumpBtnPressed { get; }
        bool IsJumpBtnStillPressed { get; }
    }

}
