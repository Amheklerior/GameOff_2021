using UnityEngine;

namespace gameoffjam {

    public sealed class PlayerAnimationController {

        private const string MOVING_SPEED_FLOAT = "Moving speed";
        private const string JUMPING_SPEED_FLOAT = "Jumping speed";
        private const string JUMP_TRIGGER = "Jump";
        private const string GROUNDED_BOOL = "Grounded";
        
        private Animator _animator;

        public PlayerAnimationController(Animator animator) {
            _animator = animator;
        }

        public void StartJumpAnimation() => _animator.SetTrigger(JUMP_TRIGGER);
        public void UpdateGroundedCheck(bool isGrounded) => _animator.SetBool(GROUNDED_BOOL, isGrounded);
        public void UpdateJumpingSpeed(float speed) => _animator.SetFloat(JUMPING_SPEED_FLOAT, speed);
        public void UpdateMovingSpeed(float speed) => _animator.SetFloat(MOVING_SPEED_FLOAT, speed);

    }

} 
