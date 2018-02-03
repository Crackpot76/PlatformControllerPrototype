using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class JumpAirState: IStateInterface {
        
        const float accelerationTime = 0.1f;

        public void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController) {
            animator.SetBool(AnimPlayerParamters.JUMP_AIR, true);
            
            Move(stateMachine, ref playerController);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController) {
            // Move while jumping
            Move(stateMachine, ref playerController);
            if (playerController.IsFalling() || playerController.IsGrounded()) {
                return PlayerStateMachine.fallingState;
            }
            return null;
        }

        private void Move(PlayerStateMachine stateMachine, ref PlayerController playerController) {
            float directionX = Input.GetAxisRaw("Horizontal");
            stateMachine.FlipSprite(directionX);
            playerController.OnMoving(directionX, accelerationTime, 1);
        }

        public void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController) {
            animator.SetBool(AnimPlayerParamters.JUMP_AIR, false);
        }

        public void OnAnimEvent(string parameter) {
            // not implemented
        }
    }
}