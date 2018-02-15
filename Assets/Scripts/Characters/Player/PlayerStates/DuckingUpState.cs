using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class DuckingUpState: AbstractState {

        private bool animationHasStopped;

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            if (animationHasStopped) {

                if (playerController.IsFalling()) {
                    return PlayerStateMachine.fallingState;
                }

                float directionX = Input.GetAxisRaw("Horizontal");
                if (directionX == 0) {
                    return PlayerStateMachine.idleState;
                } else {
                    return PlayerStateMachine.runningState;
                }
            }
            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
        }

        public override void OnAnimEvent(PlayerStateMachine stateMachine, string parameter) {
            animationHasStopped = true;
        }
    }
}