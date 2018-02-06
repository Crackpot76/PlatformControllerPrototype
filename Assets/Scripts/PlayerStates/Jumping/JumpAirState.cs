using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class JumpAirState: AbstractStateAir {
        

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.JUMP_AIR, true);            
            Move(stateMachine, playerController);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            Debug.Log("MoveMultiplierAir: " + moveMultiplierAir);
            if (playerController.IsFalling() || playerController.IsGrounded()) {
                float directionX = Input.GetAxisRaw("Horizontal");

                if (directionX == 0) {
                    PlayerStateMachine.fallingIdleState.InitParameters(initialRunJumpDirectionX, moveMultiplierAir);
                    return PlayerStateMachine.fallingIdleState;
                } else {
                    PlayerStateMachine.fallingRunningState.InitParameters(initialRunJumpDirectionX, moveMultiplierAir);
                    return PlayerStateMachine.fallingRunningState;
                }
            }

            // Move while jumping
            Move(stateMachine, playerController);
            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.JUMP_AIR, false);
            ResetStateAir();
        }        
    }
}