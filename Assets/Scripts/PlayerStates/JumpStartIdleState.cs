using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class JumpStartIdleState: IStateInterface {

        bool animationHasStopped;
        bool animationIsAirborne;
        bool onJumpDownTriggered;

        const float accelerationTime = 0.1f;

        public void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParamters.JUMP_START_IDLE, true);
            
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {

            if (playerController.IsGrounded()) {
               // Debug.Log("Allready grounded!!!");
            }

            if (animationHasStopped) {
                return PlayerStateMachine.jumpAirState;
            }

            // Move while jumping
            Move(stateMachine, ref playerController);

            return null;
        }

        private void Move(PlayerStateMachine stateMachine, ref PlayerMovementController playerController) {
            float directionX = Input.GetAxisRaw("Horizontal");
            stateMachine.FlipSprite(directionX);
            playerController.OnMoving(directionX, accelerationTime, 1.4f);
        }

        public void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParamters.JUMP_START_IDLE, false);
        }

        public void OnAnimEvent(string parameter) {
            animationHasStopped = true;
        }
    }
}