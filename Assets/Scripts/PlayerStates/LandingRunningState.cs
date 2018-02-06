using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class LandingRunningState: IStateInterface {

        const float accelerationTime = 0.1f;
        bool animationHasStopped;
        float runJumpDirectionX = 1f;

        public void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParamters.LANDING_RUNNING, true);
            Move(ref playerController);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {

            Debug.Log(">>>> LANDING RUNNING! MOVE DIR:" + runJumpDirectionX);
            if (animationHasStopped) {
                float directionX = Input.GetAxisRaw("Horizontal");

                if (directionX == 0) {
                    return PlayerStateMachine.stoppingState;
                } else {
                    return PlayerStateMachine.runningState;
                }                
            }

            Move(ref playerController);

            return null;
        }

        private void Move(ref PlayerMovementController playerController) {
            playerController.OnMoving(runJumpDirectionX, accelerationTime);
        }

        public void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {
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