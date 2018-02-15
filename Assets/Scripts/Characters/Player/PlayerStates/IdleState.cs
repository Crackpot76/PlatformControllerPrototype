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
            
            if (Input.GetKey(KeyCode.Space) && playerController.IsJumpingPossible()) {
                return PlayerStateMachine.preJumpIdleState;
            }
            
            if (Input.GetAxisRaw("Horizontal") != 0 && playerController.IsGrounded()) {
                return PlayerStateMachine.runningState;
            }

            if (playerController.IsFalling()) {
                return PlayerStateMachine.fallingState;
            }

            if (Input.GetKey(KeyCode.DownArrow) && playerController.IsGrounded()) {
                return PlayerStateMachine.duckingState;
            }

            if (Input.GetKey(KeyCode.D) && playerController.IsGrounded()) {
                return PlayerStateMachine.attackingLightState;
            }
            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController)
        {
            animator.SetBool(AnimPlayerParameters.IDLE, false);
        }        
    }
}