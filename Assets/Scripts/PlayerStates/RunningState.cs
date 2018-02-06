using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class RunningState : AbstractState {
        

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {

            animator.SetBool(AnimPlayerParameters.RUNNING, true);
            MoveXGrounded(stateMachine, playerController);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            if (Input.GetKey(KeyCode.Space)) {
                return PlayerStateMachine.preJumpRunningState;
            }

            float directionX = Input.GetAxisRaw("Horizontal");
            if (directionX == 0 || directionX != stateMachine.currentDirectionX) {
                // Stopping
                return PlayerStateMachine.stoppingState;
            } 

            if (playerController.IsFalling()) {
                return PlayerStateMachine.fallingRunningState;
            }

            // continue moving
            MoveXGrounded(stateMachine, playerController);

            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            animator.SetBool(AnimPlayerParameters.RUNNING, false);
        }
    }
}