using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class IdleState : IStateInterface {

        public void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController)
        {
            animator.SetBool(AnimPlayerParamters.IDLE, true);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController)
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            if (directionX != 0) {
                return PlayerStateMachine.runningState;
            }

            if (Input.GetKeyDown(KeyCode.Space) && playerController.IsJumpingPossible()) {
                return PlayerStateMachine.preJumpIdleState;
            }
            return null;
        }

        public void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController)
        {
            animator.SetBool(AnimPlayerParamters.IDLE, false);
        }

        public void OnAnimEvent(string parameter) {
            // not implemented
        }
    }
}