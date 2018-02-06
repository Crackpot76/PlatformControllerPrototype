using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class LandingIdleState: IStateInterface {

        bool animationHasStopped;

        public void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParamters.LANDING_IDLE, true);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {

            if (Input.GetKeyDown(KeyCode.Space)) {
                return PlayerStateMachine.preJumpIdleState;
            }
            if (animationHasStopped) {
                return PlayerStateMachine.idleState;
            }

            return null;
        }

        public void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParamters.LANDING_IDLE, false);
        }

        public void OnAnimEvent(string parameter) {
            animationHasStopped = true;
        }
        
    }
}