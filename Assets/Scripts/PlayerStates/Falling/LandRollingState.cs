using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class LandRollingState: AbstractState {

        bool animationHasStopped;

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParameters.LAND_ROLLING, true);
            MoveXRaw(playerController, stateMachine.currentDirectionX, ACCELERATION_TIME_GROUNDED, 1f);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {

            if (animationHasStopped) {
                float directionX = Input.GetAxisRaw("Horizontal");

                if (directionX == 0) {
                    return PlayerStateMachine.stoppingState;
                } else {
                    return PlayerStateMachine.runningState;
                }
            }

            // Automatic rolling forward, no Input required
            MoveXRaw(playerController, stateMachine.currentDirectionX, ACCELERATION_TIME_GROUNDED, 1f);

            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.LAND_ROLLING, false);
        }

        public override void OnAnimEvent(string parameter) {
            animationHasStopped = true;
        }
    }
}