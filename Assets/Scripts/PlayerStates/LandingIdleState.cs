using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class LandingIdleState: IStateInterface {

        const float accelerationTime = 0.1f;
        bool animationHasStopped;

        public void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {
            animationHasStopped = false;
            animator.SetTrigger(AnimPlayerParamters.LANDING_IDLE_TRIGGER);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {

            if (Input.GetKeyDown(KeyCode.Space)) {
                return PlayerStateMachine.preJumpIdleState;
            }
            if (animationHasStopped) {
                return PlayerStateMachine.idleState;
            }

            return null;
        }

        public void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {
        }

        public void OnAnimEvent(string parameter) {
            animationHasStopped = true;
        }
    }
}