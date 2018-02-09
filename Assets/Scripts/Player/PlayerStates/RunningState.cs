using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class RunningState : AbstractState {

        const float QUICKSTOP_DISTANCE = 2.5f;
        float startX;

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            startX = stateMachine.currentTransform.position.x;
            animator.SetBool(AnimPlayerParameters.RUNNING, true);
            MoveXGrounded(stateMachine, playerController);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            if (playerController.IsFalling()) {
                return PlayerStateMachine.fallingState;
            }

            if (Input.GetKey(KeyCode.Space)) {
                return PlayerStateMachine.preJumpRunningState;
            }

            float directionX = Input.GetAxisRaw("Horizontal");
            if (directionX == 0 || directionX != stateMachine.currentDirectionX) {

                //check if stopping Animation
                float newPosX = stateMachine.currentTransform.position.x;
                float distanceRun = (startX >= newPosX ? startX - newPosX : newPosX - startX);
                if (distanceRun < QUICKSTOP_DISTANCE) {
                    if (directionX == 0) {
                        return PlayerStateMachine.idleState;
                    } else {
                        // Continue moving, stay in State
                    }
                } else {
                    // Stopping
                    return PlayerStateMachine.stoppingState;
                }
            } 



            // continue moving
            MoveXGrounded(stateMachine, playerController);

            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            animator.SetBool(AnimPlayerParameters.RUNNING, false);
        }
    }
}