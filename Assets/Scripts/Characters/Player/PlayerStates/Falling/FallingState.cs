using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public  class FallingState: AbstractStateAir {

        float minFallHeight;
        float startYPos;
        float distanceFalling = 0f;


        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.FALLING, true);
            minFallHeight = 1f;
            startYPos = stateMachine.transform.position.y;
            distanceFalling = 0f;
            Move(stateMachine, playerController);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            float directionX = Input.GetAxisRaw("Horizontal");

            if (playerController.IsGrounded()) {
                float currentYPos = stateMachine.transform.position.y;
                distanceFalling = Mathf.Abs(Mathf.Abs(startYPos) - Mathf.Abs(currentYPos));

                // Schnelle Bewegung beim springen, also abrollen
                if (moveMultiplierAir >= MIDDLE_MOVE_FACTOR_AIR) {
                    return PlayerStateMachine.landRollingState;
                }

                
                if (distanceFalling < minFallHeight) {
                    Debug.Log("Land in Idle" + distanceFalling + " < " + minFallHeight);

                    return PlayerStateMachine.idleState;
                }

                if (directionX == 0) {
                    Debug.Log("Land in landIdle");
                    return PlayerStateMachine.landIdleState;
                }
                else {
                    Debug.Log("Land in landRunning");
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
            CameraShakeImpact(stateMachine);
        }

        private void CameraShakeImpact(PlayerStateMachine stateMachine) {
            float maxJump = stateMachine.movementController.maxJumpHeight;
            float minJump = stateMachine.movementController.minJumpHeight;

            if (distanceFalling > minJump * 1.1f) {
                float jumpPercent = (distanceFalling - minFallHeight) / (maxJump - minFallHeight);
                // 0.3f = 100%
                stateMachine.ShakeCamera(0.3f * jumpPercent, 10, 0.15f);
            }
        }
    }
}