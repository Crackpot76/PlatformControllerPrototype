using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class JumpStartRunningState: IStateInterface {

        bool animationHasStopped;
        bool animationIsAirborne;
        bool onJumpDownTriggered;
        float moveAddonPercent = 0f;

        const float accelerationTime = 0.1f;

        public void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParamters.JUMP_START_RUNNING, true);

        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {

            if (playerController.IsGrounded()) {
                // Debug.Log("Allready grounded!!!");
            }

            // Move while jumping
            Move(stateMachine, ref playerController);

            if (animationHasStopped) {
                PlayerStateMachine.jumpAirState.SetMoveAddonPercent(moveAddonPercent);
                return PlayerStateMachine.jumpAirState;
            }
            return null;
        }

        private void Move(PlayerStateMachine stateMachine, ref PlayerMovementController playerController) {
            float directionX = Input.GetAxisRaw("Horizontal");
            stateMachine.FlipSprite(directionX);
            playerController.OnMoving(directionX, accelerationTime, moveAddonPercent);
        }

        public void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParamters.JUMP_START_RUNNING, false);
            moveAddonPercent = 0f;
        }

        public void OnAnimEvent(string parameter) {
            animationHasStopped = true;
        }

        public void SetMoveAddonPercent(float newMoveAddonPercent) {
            moveAddonPercent = newMoveAddonPercent;
        }
    }
}