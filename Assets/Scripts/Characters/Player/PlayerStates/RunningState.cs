using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class RunningState : AbstractState {

        private const string DUST_RUN_EFFECT_PREFAB_NAME = "DustRunningGO";
        private const float QUICKSTOP_DISTANCE = 2.5f;

        private Object dustEffect;
        private float startX;

        public RunningState() {
            //Init Effect Prefab
            dustEffect = Resources.Load(DUST_RUN_EFFECT_PREFAB_NAME);
        }

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController)
        {
            startX = stateMachine.transform.position.x;
            animator.SetBool(AnimPlayerParameters.RUNNING, true);
            MoveXGrounded(stateMachine, playerController);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController)
        {
            if (playerController.IsFalling()) {
                return PlayerStateMachine.fallingState;
            }

            if (Input.GetKey(KeyCode.Space)) {
                return PlayerStateMachine.preJumpRunningState;
            }

            if (Input.GetKey(KeyCode.T) && playerController.IsGrounded()) {
                //return PlayerStateMachine.attackingHeavyState;
                return PlayerStateMachine.attackingCombo1State;
            }

            float directionX = Input.GetAxisRaw("Horizontal");
            if (directionX == 0 || directionX != stateMachine.currentDirectionX) {

                //check if stopping Animation
                float newPosX = stateMachine.transform.position.x;
                float distanceRun = (startX >= newPosX ? startX - newPosX : newPosX - startX);
                if (distanceRun < QUICKSTOP_DISTANCE) {
                    if (directionX == 0) {
                        return PlayerStateMachine.idleState;
                    } else {
                        // Continue moving, stay in State
                    }
                } else {
                    // Stopping
                    return PlayerStateMachine.stoppingState;
                }
            } 



            // continue moving
            MoveXGrounded(stateMachine, playerController);

            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController)
        {
            animator.SetBool(AnimPlayerParameters.RUNNING, false);
        }

        public override void OnAnimEvent(PlayerStateMachine stateMachine, string parameter) {
            stateMachine.InstantiateEffect(dustEffect);
        }
    }
}