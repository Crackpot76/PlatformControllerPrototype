using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class ClimbingState: AbstractState {

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.CLIMBING, true);
            if (Input.GetKey(KeyCode.UpArrow)) {
                playerController.OnClimbLadder(1);
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                playerController.OnClimbLadder(-1);
            }
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            if (!playerController.LadderBelow()) {
                return PlayerStateMachine.idleState;
            }
            if (Input.GetKey(KeyCode.UpArrow)) {
                playerController.OnClimbLadder(1);
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                playerController.OnClimbLadder(-1);
            }

            if (playerController.IsGroundBelow()) {
                Debug.Log("Unten angekommen");
                return PlayerStateMachine.idleState;
            }
            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.CLIMBING, false);
        }
    }
}