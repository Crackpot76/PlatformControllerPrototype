using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class PreJumpIdleState: AbstractState {


        const float accelerationTime = 0.1f;
        const float maxJumpTime = 0.6f;

        float jumpTimerStart;


        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.PRE_JUMP_IDLE, true);
            jumpTimerStart = Time.time;
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {

            if (!playerController.IsJumpingPossible()) {
                return PlayerStateMachine.idleState;
            }

            if (Input.GetKeyUp(KeyCode.Space)) {                

                float jumpTime = Time.time - jumpTimerStart;
                float jumpForcePercent = CalculatePercentFromJumpTimer(jumpTime);

                if (jumpForcePercent > 0) {
                    // JUMP NOW!
                    playerController.OnJumping(jumpForcePercent);

                    float directionX = Input.GetAxisRaw("Horizontal");
                    if (directionX != 0) {
                        // Spieler will sich beim springen bewegen
                        stateMachine.FlipSprite(directionX);
                        PlayerStateMachine.jumpStartRunningState.InitParameters(directionX);
                        return PlayerStateMachine.jumpStartRunningState;
                    } else {
                        return PlayerStateMachine.jumpStartIdleState;
                    }
                    
                } else {
                    Debug.LogError("Error in PreJumpIdleState: JumpForce <= 0, also kein Sprung möglich!"); 
                    return PlayerStateMachine.idleState;
                }            

            } else {
                if (!Input.GetKey(KeyCode.Space)) {
                    Debug.LogError("Error in PreJumpIdleState: Space Taste nicht mehr gedrückt, aber kein KeyUp!");
                    return PlayerStateMachine.idleState;
                }
            }
            
            return null;
        }



        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.PRE_JUMP_IDLE, false);
        }

        // Calculates percent of jump force from prepared jumping time
        private float CalculatePercentFromJumpTimer(float jumpTime) {
            float result = 0;
            float clampedJumpTime = Mathf.Clamp(jumpTime, 0, maxJumpTime);
            result = (clampedJumpTime * (100 / maxJumpTime)) / 100;
            return result;
        }
    }
}