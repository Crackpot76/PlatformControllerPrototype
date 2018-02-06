using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class RunningState : IStateInterface {

        const float accelerationTime = 0.04f;

        public void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController)
        {

            animator.SetBool(AnimPlayerParamters.RUNNING, true);
            float directionX = Input.GetAxisRaw("Horizontal");
            Move(directionX, stateMachine, ref playerController);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController)
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            if (directionX == 0 || directionX != stateMachine.currentDirectionX) {
                // Stopping
                return PlayerStateMachine.stoppingState;
            } 

            if (Input.GetKeyDown(KeyCode.Space)) {
                return PlayerStateMachine.preJumpRunningState;
            }

            if (playerController.IsFalling()) {
                PlayerStateMachine.fallingRunningState.InitParameter(directionX, 1f);
                return PlayerStateMachine.fallingRunningState;
            }

            // continue moving
            Move(directionX, stateMachine, ref playerController);
            return null;
        }



        private void Move(float directionX, PlayerStateMachine stateMachine, ref PlayerMovementController playerController) {
            stateMachine.FlipSprite(directionX);
            playerController.OnMoving(directionX, accelerationTime);            
        }

        public void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController)
        {
            animator.SetBool(AnimPlayerParamters.RUNNING, false);
        }

        public void OnAnimEvent(string parameter) {
            // not implemented
        }
    }
}