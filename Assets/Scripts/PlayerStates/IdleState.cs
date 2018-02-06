using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class IdleState : AbstractState {

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            animator.SetBool(AnimPlayerParameters.IDLE, true);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            
            if (Input.GetKey(KeyCode.Space) && playerController.IsJumpingPossible()) {
                return PlayerStateMachine.preJumpIdleState;
            }
            
            if (Input.GetAxisRaw("Horizontal") != 0) {
                return PlayerStateMachine.runningState;
            }

            if (playerController.IsFalling()) {
                return PlayerStateMachine.fallingIdleState;
            }
            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            animator.SetBool(AnimPlayerParameters.IDLE, false);
        }        
    }
}