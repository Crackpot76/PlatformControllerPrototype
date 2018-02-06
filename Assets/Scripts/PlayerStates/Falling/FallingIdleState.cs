using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public  class FallingIdleState: AbstractStateAir {   

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.FALLING_IDLE, true);
            Move(stateMachine, playerController);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {

            float directionX = Input.GetAxisRaw("Horizontal");

            if (playerController.IsGrounded()) {
                if (directionX == 0 || moveMultiplierAir < MIDDLE_MOVE_FACTOR_AIR) {
                    return PlayerStateMachine.landingIdleState;
                } else {
                    return PlayerStateMachine.landingRunningState;
                }
            }

            if (directionX != 0) {
                return PlayerStateMachine.fallingRunningState;
            }

            // Move while jumping
            Move(stateMachine, playerController);

            return null;
        }


        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.FALLING_IDLE, false);
            ResetStateAir();
        }
    }
}