using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class IdleState : AbstractState {

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController)
        {
            animator.SetBool(AnimPlayerParameters.IDLE, true);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController)
        {

            if (Input.GetKey(KeyCode.UpArrow) && playerController.LadderBelow()) {
                playerController.OnClimbLadder(1);
            }
            if (Input.GetKey(KeyCode.DownArrow) && playerController.IsGrounded()) {
                if (playerController.LadderBelow()) {
                    playerController.OnClimbLadder(-1);
                } else {
                    return PlayerStateMachine.duckingState;
                }
            }

            if (Input.GetKey(KeyCode.Space) && playerController.IsJumpingPossible()) {
                return PlayerStateMachine.preJumpIdleState;
            }

            if (Input.GetAxisRaw("Horizontal") != 0 && playerController.IsGrounded()) {
                return PlayerStateMachine.runningState;
            }

            if (playerController.IsFalling()) {
                return PlayerStateMachine.fallingState;
            }



            if (stateMachine.inputController.StartHeavyAttack()) { //TODO erst "aufladen", dann angriff
                return PlayerStateMachine.attackingHeavyState;
            }
            if (stateMachine.inputController.IsLightAttack()) {
                return PlayerStateMachine.attackingCombo1State;
            }
            
            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController)
        {
            animator.SetBool(AnimPlayerParameters.IDLE, false);
        }        
    }
}