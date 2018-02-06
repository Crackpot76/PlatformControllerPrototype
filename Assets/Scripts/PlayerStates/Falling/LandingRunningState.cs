using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class LandingRunningState: IStateInterface {

        const float accelerationTime = 0.1f;
        bool animationHasStopped;
        float runJumpDirectionX = 1f;

        public void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParamters.LANDING_RUNNING, true);
            Move(playerController);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            
            if (animationHasStopped) {
                float directionX = Input.GetAxisRaw("Horizontal");

                if (directionX == 0) {
                    return PlayerStateMachine.stoppingState;
                } else {
                    return PlayerStateMachine.runningState;
                }                
            }

            Move(playerController);

            return null;
        }

        private void Move(PlayerMovementController playerController) {
            playerController.OnMoving(runJumpDirectionX, accelerationTime);
        }

        public void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParamters.LANDING_RUNNING, false);
            runJumpDirectionX = 1;
        }

        public void OnAnimEvent(string parameter) {
            animationHasStopped = true;
        }

        public void InitParameter(float runJumpDirectionX) {
            this.runJumpDirectionX = runJumpDirectionX;
        }
    }
}