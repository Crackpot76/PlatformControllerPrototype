using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class JumpAirState: IStateInterface {
        
        const float accelerationTime = 0.1f;

        float runJumpDirectionX = 0f;
        float moveMultiplierAir = 1f;

        public void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParamters.JUMP_AIR, true);
            
            Move(stateMachine, playerController);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            
            if (playerController.IsFalling() || playerController.IsGrounded()) {
                float directionX = Input.GetAxisRaw("Horizontal");

                if (directionX == 0) {
                    PlayerStateMachine.fallingIdleState.InitParameter(runJumpDirectionX, moveMultiplierAir);
                    return PlayerStateMachine.fallingIdleState;
                } else {
                    PlayerStateMachine.fallingRunningState.InitParameter(runJumpDirectionX, moveMultiplierAir);
                    return PlayerStateMachine.fallingRunningState;
                }
            }

            // Move while jumping
            Move(stateMachine, playerController);
            return null;
        }

        private void Move(PlayerStateMachine stateMachine, PlayerMovementController playerController) {
            if (runJumpDirectionX != Input.GetAxisRaw("Horizontal")) {
                runJumpDirectionX = Input.GetAxisRaw("Horizontal");
                stateMachine.FlipSprite(runJumpDirectionX);
                moveMultiplierAir = 1f; // wieder initialisieren da Initialrichtung gewechselt
            }
            playerController.OnMoving(runJumpDirectionX, accelerationTime, moveMultiplierAir);
        }

        public void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParamters.JUMP_AIR, false);
            runJumpDirectionX = 0f;
            moveMultiplierAir = 1f;
        }

        public void OnAnimEvent(string parameter) {
            // not implemented
        }

        public void InitParameter(float runJumpDirectionX, float moveMultiplierAir) {
            this.runJumpDirectionX = runJumpDirectionX;
            this.moveMultiplierAir = moveMultiplierAir;
        }
    }
}