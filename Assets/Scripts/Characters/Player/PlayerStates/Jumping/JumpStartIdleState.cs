using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class JumpStartIdleState: AbstractStateAir {

        
        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.JUMP_START_IDLE, true);
            MoveXAirborne(stateMachine, playerController);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            if (playerController.IsGrounded()) {
               return PlayerStateMachine.idleState;
            }

            if (playerController.IsFalling() || playerController.IsGrounded()) {
                PlayerStateMachine.fallingState.InitParameters(stateMachine.currentDirectionX, moveMultiplierAir);
                return PlayerStateMachine.fallingState;
            }

            // Move while jumping
            Move(stateMachine, playerController);

            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.JUMP_START_IDLE, false);
            ResetStateAir();
        }
        
    }
}