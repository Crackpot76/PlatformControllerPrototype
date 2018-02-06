using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class JumpStartIdleState: AbstractState {

        bool animationHasStopped;
        bool animationIsAirborne;

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParameters.JUMP_START_IDLE, true);
            MoveXAirborne(stateMachine, playerController);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {

            if (playerController.IsGrounded()) {
               // Debug.Log("Allready grounded!!!");
            }

            if (animationHasStopped) {
                return PlayerStateMachine.jumpAirState;
            }

            // Move while jumping
            MoveXAirborne(stateMachine, playerController);

            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.JUMP_START_IDLE, false);
        }

        public override void OnAnimEvent(string parameter) {
            animationHasStopped = true;
        }
    }
}