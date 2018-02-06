using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class RunningState : IStateInterface {

        const float accelerationTime = 0.04f;

        public void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {

            animator.SetBool(AnimPlayerParamters.RUNNING, true);
            float directionX = Input.GetAxisRaw("Horizontal");
            Move(directionX, stateMachine, playerController);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            float directionX = Input.GetAxisRaw("Horizontal");

            if (Input.GetKey(KeyCode.Space)) {
                return PlayerStateMachine.preJumpRunningState;
            }

            if (directionX == 0 || directionX != stateMachine.currentDirectionX) {
                // Stopping
                return PlayerStateMachine.stoppingState;
            } 



            if (playerController.IsFalling()) {
                PlayerStateMachine.fallingRunningState.InitParameter(directionX, 1f);
                return PlayerStateMachine.fallingRunningState;
            }

            // continue moving
            Move(directionX, stateMachine, playerController);
            return null;
        }



        private void Move(float directionX, PlayerStateMachine stateMachine, PlayerMovementController playerController) {
            stateMachine.FlipSprite(directionX);
            playerController.OnMoving(directionX, accelerationTime);            
        }

        public void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            animator.SetBool(AnimPlayerParamters.RUNNING, false);
        }

        public void OnAnimEvent(string parameter) {
            // not implemented
        }
    }
}