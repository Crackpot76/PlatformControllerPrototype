using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class StoppingState : AbstractState {

        bool animationHasStopped;

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParameters.STOPPING, true);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
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

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.STOPPING, false);
        }

        public override void OnAnimEvent(string parameter) {
            animationHasStopped = true;
        }
    }
}