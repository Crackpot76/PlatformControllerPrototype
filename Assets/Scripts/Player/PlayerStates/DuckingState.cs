using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class DuckingState: AbstractState {
        
        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.DUCKING, true);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            if (!Input.GetKey(KeyCode.DownArrow) || playerController.IsFalling()) {
                return PlayerStateMachine.duckingUpState;
            }
            
            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.DUCKING, false);
        }
    }
}