using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class IdleState : IStateInterface {

        public void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            animator.SetBool(AnimPlayerParamters.IDLE, true);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            if (Input.GetKey(KeyCode.Space) && playerController.IsJumpingPossible()) {
                return PlayerStateMachine.preJumpIdleState;
            }

            if (directionX != 0) {
                return PlayerStateMachine.runningState;
            }

            if (playerController.IsFalling()) {
                return PlayerStateMachine.fallingIdleState;
            }
            return null;
        }

        public void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            animator.SetBool(AnimPlayerParamters.IDLE, false);
        }

        public void OnAnimEvent(string parameter) {
            // not implemented
        }
    }
}