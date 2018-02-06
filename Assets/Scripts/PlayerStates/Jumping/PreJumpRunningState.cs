using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class PreJumpRunningState: IStateInterface {


        const float accelerationTime = 0.2f;
        const float maxJumpTime = 0.6f;
        const float maxJumpPercent = 90;
        const float moveFactor = 1.4f;
        const float moveFactorAir = 2f;

        float jumpTimerStart;
        

        public void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParamters.PRE_JUMP_RUNNING, true);
            jumpTimerStart = Time.time;

            float directionX = Input.GetAxisRaw("Horizontal");
            Move(directionX, stateMachine, playerController);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {

            float directionX = Input.GetAxisRaw("Horizontal");
            if (directionX == 0 || directionX != stateMachine.currentDirectionX || !playerController.IsJumpingPossible()) {
                // Stopping
                return PlayerStateMachine.stoppingState;
            }

            

            if (Input.GetKeyUp(KeyCode.Space)) {
                float jumpTime = Time.time - jumpTimerStart;
                float jumpForcePercent = CalculatePercentFromJumpTimer(jumpTime);

                if (jumpForcePercent > 0) {
                    // JUMP NOW!
                    playerController.OnJumping(jumpForcePercent);
                    PlayerStateMachine.jumpStartRunningState.InitParameters(directionX, moveFactorAir);
                    return PlayerStateMachine.jumpStartRunningState;
                } else {
                    Debug.LogError("Error in PreJumpIdleState: JumpForce <= 0, also kein Sprung möglich!");
                    return PlayerStateMachine.idleState;
                }

            } else {
                if (!Input.GetKey(KeyCode.Space)) {
                    Debug.LogError("Error in PreJumpIdleState: Space Taste nicht mehr gedrückt, aber kein KeyUp!");
                    return PlayerStateMachine.runningState;
                }
            }

            // continue moving
            Move(directionX, stateMachine, playerController);

            return null;
        }

        private void Move(float directionX, PlayerStateMachine stateMachine, PlayerMovementController playerController) {
            stateMachine.FlipSprite(directionX);
            playerController.OnMoving(directionX, accelerationTime, moveFactor);
        }

        // Calculates percent of jump force y from prepared jumping time
        private float CalculatePercentFromJumpTimer(float jumpTime) {
            float result = 0;
            float clampedJumpTime = Mathf.Clamp(jumpTime, 0, maxJumpTime);
            result = (clampedJumpTime * (maxJumpPercent / maxJumpTime)) / 100;
            return result;
        }


        public void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParamters.PRE_JUMP_RUNNING, false);
        }

        public void OnAnimEvent(string parameter) {
            // not implemented
        }
    }
}
