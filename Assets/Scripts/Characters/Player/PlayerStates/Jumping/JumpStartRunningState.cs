using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class JumpStartRunningState: AbstractStateAir {
        

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.JUMP_START_RUNNING, true);
            MoveXAirborne(stateMachine, playerController);
            SoundManager.PlaySFX(stateMachine.sounds.heavyAttack);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            if (playerController.IsGrounded()) {
                return PlayerStateMachine.runningState;
            }

            if (playerController.IsFalling() || playerController.IsGrounded()) {
                PlayerStateMachine.fallingState.InitParameters(initialRunJumpDirectionX, moveMultiplierAir);
                return PlayerStateMachine.fallingState;
            }

            // Move while jumping
            MoveXAirborne(stateMachine, playerController);
            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.JUMP_START_RUNNING, false);
            ResetStateAir();
        }
        
    }
}