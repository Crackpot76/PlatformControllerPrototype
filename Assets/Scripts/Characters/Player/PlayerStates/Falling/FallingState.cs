using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public  class FallingState: AbstractStateAir {

        const float MIN_FALL_HEIGHT = 1f;
        float startYPos;


        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.FALLING, true);
            startYPos = stateMachine.currentTransform.position.y;
            Move(stateMachine, playerController);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            float directionX = Input.GetAxisRaw("Horizontal");

            if (playerController.IsGrounded()) {


                // Schnelle Bewegung beim springen, also abrollen
                if (moveMultiplierAir >= MIDDLE_MOVE_FACTOR_AIR) {
                    return PlayerStateMachine.landRollingState;
                }

                float currentYPos = stateMachine.currentTransform.position.y;
                float distanceFalling = Mathf.Abs(Mathf.Abs(startYPos) - Mathf.Abs(currentYPos));
                if (distanceFalling < MIN_FALL_HEIGHT) {
                    return PlayerStateMachine.idleState;
                }

                if (directionX == 0) {
                    return PlayerStateMachine.landIdleState;
                }
                else {
                    return PlayerStateMachine.landRunningState;
                }
            }            

            // Move while jumping
            Move(stateMachine, playerController);

            return null;
        }


        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.FALLING, false);
            ResetStateAir();
        }
    }
}