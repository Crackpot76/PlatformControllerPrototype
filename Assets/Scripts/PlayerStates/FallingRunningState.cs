using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class FallingRunningState: IStateInterface {

        const float accelerationTime = 0.1f;

        float runJumpDirectionX = 0f;
        float moveMultiplierAir = 1f;

        public void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParamters.FALLING_RUNNING, true);
            Move(stateMachine, ref playerController);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {

            float directionX = Input.GetAxisRaw("Horizontal");

            if (playerController.IsGrounded()) {               
                if (directionX == 0) {
                    return PlayerStateMachine.landingIdleState;
                } else {
                    PlayerStateMachine.landingRunningState.InitParameter(runJumpDirectionX);
                    return PlayerStateMachine.landingRunningState;
                }                
            } 

            if (directionX == 0) {
                return PlayerStateMachine.fallingIdleState;
            }

            // Move while jumping
            Move(stateMachine, ref playerController);

            return null;
        }

        private void Move(PlayerStateMachine stateMachine, ref PlayerMovementController playerController) {
            if (runJumpDirectionX != Input.GetAxisRaw("Horizontal")) {
                runJumpDirectionX = Input.GetAxisRaw("Horizontal");
                stateMachine.FlipSprite(runJumpDirectionX);
                moveMultiplierAir = 1f; // wieder initialisieren da Initialrichtung gewechselt
            }
            playerController.OnMoving(runJumpDirectionX, accelerationTime, moveMultiplierAir);
        }

        public void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParamters.FALLING_RUNNING, false);
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