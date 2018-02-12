using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class PreJumpRunningState: AbstractState {
        
        private const string DUST_JUMP_EFFECT_PREFAB_NAME = "DustJumpGo";
        private const string DUST_RUN_EFFECT_PREFAB_NAME = "DustRunningGO";
        private const float maxJumpTime = 0.6f;
        private const float maxJumpPercent = 75f;
        private const float moveFactorGrounded = 1.4f;

        private float jumpTimerStart;
        private Object dustJumpEffect;
        private Object dustRunEffect;

        public PreJumpRunningState() {
            //Init Effect Prefab
            dustJumpEffect = Resources.Load(DUST_JUMP_EFFECT_PREFAB_NAME);
            dustRunEffect = Resources.Load(DUST_RUN_EFFECT_PREFAB_NAME);
        }

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.PRE_JUMP_RUNNING, true);
            jumpTimerStart = Time.time;
            MoveXGrounded(stateMachine, playerController, moveFactorGrounded);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {

            float directionX = Input.GetAxisRaw("Horizontal");
            if (directionX == 0 || directionX != stateMachine.currentDirectionX || !playerController.IsJumpingPossible()) {
                // Stopping
                return PlayerStateMachine.stoppingState;
            }
                   

            if (Input.GetKeyUp(KeyCode.Space)) {
                float jumpTime = Time.time - jumpTimerStart;
                float jumpForcePercent = CalculatePercentFromJumpTimer(jumpTime);
                float moveFactorAirborne = CalculateMoveFactorFromJumpTimer(jumpTime);

                if (jumpForcePercent > 0) {
                    stateMachine.InstantiateEffect(dustJumpEffect);
                    playerController.OnJumping(jumpForcePercent);                    
                    PlayerStateMachine.jumpStartRunningState.InitParameters(directionX, moveFactorAirborne);
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
            MoveXGrounded(stateMachine, playerController, moveFactorGrounded);

            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.PRE_JUMP_RUNNING, false);
        }

        public override void OnAnimEvent(PlayerStateMachine stateMachine, string parameter) {
            stateMachine.InstantiateEffect(dustRunEffect);
        }


        // Calculates percent of jump force y from prepared jumping time
        private float CalculatePercentFromJumpTimer(float jumpTime) {
            float result = 0;
            float clampedJumpTime = Mathf.Clamp(jumpTime, 0, maxJumpTime);
            result = (clampedJumpTime * (maxJumpPercent / maxJumpTime)) / 100;
            return result;
        }


        private float CalculateMoveFactorFromJumpTimer(float jumpTime) {
            float result = 0;
            float clampedJumpTime = Mathf.Clamp(jumpTime, 0, maxJumpTime);
            float differenceMoveAir = MAX_MOVE_FACTOR_AIR - MIN_MOVE_FACTOR_AIR;
            result = (clampedJumpTime * differenceMoveAir / maxJumpTime) + MIN_MOVE_FACTOR_AIR;            
            return result;
        }

    }
}
