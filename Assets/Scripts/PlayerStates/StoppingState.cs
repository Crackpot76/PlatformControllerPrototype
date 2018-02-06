using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class StoppingState : IStateInterface {

        bool animationHasStopped;

        public void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParamters.STOPPING, true);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            if (animationHasStopped) {
        
                float directionX = Input.GetAxisRaw("Horizontal");
                if (directionX == 0) {
                   return PlayerStateMachine.idleState;
                } else {
                    return PlayerStateMachine.runningState;
                }
            }
            return null;
        }

        public void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParamters.STOPPING, false);
        }

        public void OnAnimEvent(string parameter) {
            animationHasStopped = true;
        }
    }
}