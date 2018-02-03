using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class PreJumpIdleState: IStateInterface {


        const float accelerationTime = 0.1f;

        public void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController) {
            animator.SetBool(AnimPlayerParamters.PRE_JUMP_IDLE, true);
            playerController.OnJumpInputDown();
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController) {

            if (Input.GetKeyUp(KeyCode.Space)) {
                playerController.OnJumpInputUp(); //TODO hier muss noch gebastelt werden
                return PlayerStateMachine.jumpStartIdleState;
            }
            
            return null;
        }

        private void Move(PlayerStateMachine stateMachine, ref PlayerController playerController) {
            float directionX = Input.GetAxisRaw("Horizontal");
            stateMachine.FlipSprite(directionX);
            playerController.OnMoving(directionX, accelerationTime, 1);
        }

        public void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController) {
            animator.SetBool(AnimPlayerParamters.PRE_JUMP_IDLE, false);
        }

        public void OnAnimEvent(string parameter) {
            // not implemented
        }
    }
}