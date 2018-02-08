using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class DuckingState: AbstractState {

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.DUCKING, true);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {

            if (!Input.GetKey(KeyCode.DownArrow)) {
                return PlayerStateMachine.idleState;
            }

            if (playerController.IsFalling()) {
                return PlayerStateMachine.fallingState;
            }
            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.DUCKING, false);
        }
    }
}