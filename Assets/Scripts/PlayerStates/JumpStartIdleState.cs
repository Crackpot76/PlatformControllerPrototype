using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class JumpStartIdleState: IStateInterface {

        bool animationHasStopped;
        bool animationIsAirborne;
        bool onJumpDownTriggered;

        const float accelerationTime = 0.1f;

        public void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParamters.JUMP_START_IDLE, true);
            
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController) {

            if (playerController.IsGrounded()) {
               // Debug.Log("Allready grounded!!!");
            }



            // Move while jumping
            Move(stateMachine, ref playerController);



            if (animationHasStopped) {
                return PlayerStateMachine.jumpAirState;
            } 
            return null;
        }

        private void Move(PlayerStateMachine stateMachine, ref PlayerController playerController) {
            float directionX = Input.GetAxisRaw("Horizontal");
            stateMachine.FlipSprite(directionX);
            playerController.OnMoving(directionX, accelerationTime, 1);
        }

        public void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController) {
            animator.SetBool(AnimPlayerParamters.JUMP_START_IDLE, false);
        }

        public void OnAnimEvent(string parameter) {
            animationHasStopped = true;
        }
    }
}