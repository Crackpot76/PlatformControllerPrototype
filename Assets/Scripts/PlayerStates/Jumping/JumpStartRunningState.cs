using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class JumpStartRunningState: AbstractStateAir {
        
        bool animationHasStopped;

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParameters.JUMP_START_RUNNING, true);
            Move(stateMachine, playerController);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {

            if (playerController.IsGrounded()) {
                // Debug.Log("Allready grounded!!!");
            }

            if (animationHasStopped) {
                PlayerStateMachine.jumpAirState.InitParameters(initialRunJumpDirectionX, moveMultiplierAir);
                return PlayerStateMachine.jumpAirState;
            }

            // Move while jumping
            Move(stateMachine, playerController);
            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.JUMP_START_RUNNING, false);
            ResetStateAir();
        }

        public override void OnAnimEvent(string parameter) {
            animationHasStopped = true;
        }
    }
}