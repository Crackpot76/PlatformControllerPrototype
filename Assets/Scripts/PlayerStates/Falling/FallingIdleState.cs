using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class FallingIdleState: IStateInterface {

        const float accelerationTime = 0.1f;

        float runJumpDirectionX = 0f;
        float moveMultiplierAir = 1f;

        public void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParamters.FALLING_IDLE, true);
            Move(stateMachine, playerController);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
                        
            if (playerController.IsGrounded()) {
                float directionX = Input.GetAxisRaw("Horizontal");

                if (directionX == 0 || moveMultiplierAir < 2f) {
                    return PlayerStateMachine.landingIdleState;
                } else {
                    PlayerStateMachine.landingRunningState.InitParameter(runJumpDirectionX);
                    return PlayerStateMachine.landingRunningState;
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
            animator.SetBool(AnimPlayerParamters.FALLING_IDLE, false);
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