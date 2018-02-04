using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class FallingState: IStateInterface {

        const float accelerationTime = 0.1f;
        float moveAddonPercent = 0f;

        public void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParamters.FALLING, true);
            Move(stateMachine, ref playerController);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {
                        
            // Move while jumping
            Move(stateMachine, ref playerController);

            if (playerController.IsGrounded()) {
                return PlayerStateMachine.landingIdleState;
            }
            
            return null;
        }

        private void Move(PlayerStateMachine stateMachine, ref PlayerMovementController playerController) {
            float directionX = Input.GetAxisRaw("Horizontal");
            stateMachine.FlipSprite(directionX);
            playerController.OnMoving(directionX, accelerationTime, moveAddonPercent);
        }

        public void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParamters.FALLING, false);
            moveAddonPercent = 0f;
        }

        public void OnAnimEvent(string parameter) {
            // not implemented
        }

        public void SetMoveAddonPercent(float newMoveAddonPercent) {
            moveAddonPercent = newMoveAddonPercent;
        }
    }
}