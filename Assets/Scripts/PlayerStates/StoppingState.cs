using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class StoppingState : IStateInterface {

        bool animationHasStopped;

        public void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController)
        {
            animationHasStopped = false;
            animator.SetTrigger(AnimPlayerParamters.STOPPING_TRIGGER);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController)
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

        public void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController) {
        }

        public void OnAnimEvent(string parameter) {
            animationHasStopped = true;
        }
    }
}