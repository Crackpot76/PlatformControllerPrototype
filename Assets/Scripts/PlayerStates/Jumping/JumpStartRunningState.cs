using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class JumpStartRunningState: IStateInterface {

        const float accelerationTime = 0.1f;
        bool animationHasStopped;
        float runJumpDirectionX = 0f;        
        float moveMultiplierAir = 1f;

        public void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParamters.JUMP_START_RUNNING, true);
            Move(stateMachine, playerController);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {

            if (playerController.IsGrounded()) {
                // Debug.Log("Allready grounded!!!");
            }

            if (animationHasStopped) {
                PlayerStateMachine.jumpAirState.InitParameter(runJumpDirectionX, moveMultiplierAir);
                return PlayerStateMachine.jumpAirState;
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
            animator.SetBool(AnimPlayerParamters.JUMP_START_RUNNING, false);
            runJumpDirectionX = 0f;
            moveMultiplierAir = 1f;
        }

        public void OnAnimEvent(string parameter) {
            animationHasStopped = true;
        }

        public void InitParameters(float runJumpDirectionX, float moveMultiplierAir) {
            this.runJumpDirectionX = runJumpDirectionX;
            this.moveMultiplierAir = moveMultiplierAir;
        }
    }
}