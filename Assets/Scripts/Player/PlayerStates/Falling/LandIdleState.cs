using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class LandIdleState: AbstractState {

        bool animationHasStopped;

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParameters.LAND_IDLE, true);
            stateMachine.InstantiateEffect(stateMachine.dustLandingEffect);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {

            if (Input.GetKeyDown(KeyCode.Space)) {
                return PlayerStateMachine.preJumpIdleState;
            }
            if (animationHasStopped) {
                return PlayerStateMachine.idleState;
            }

            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.LAND_IDLE, false);
        }

        public override void OnAnimEvent(string parameter) {
            animationHasStopped = true;
        }
        
    }
}