using UnityEngine;
using System.Collections;

namespace PlayerStates {
   public abstract class AbstractState {

        public const float ACCELERATION_TIME_GROUNDED = 0.04f;
        public const float ACCELERATION_TIME_AIRBORNE = 0.1f;
        public const float MIN_MOVE_FACTOR_AIR = 1.5f;
        public const float MAX_MOVE_FACTOR_AIR = 2.5f;
        public const float MIDDLE_MOVE_FACTOR_AIR = (MAX_MOVE_FACTOR_AIR - MIN_MOVE_FACTOR_AIR) / 2 + MIN_MOVE_FACTOR_AIR;


        public abstract void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController);
        public abstract AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController);
        public abstract void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController);

        public virtual void OnAnimEvent(string parameter) {
            // not implemented
        }

        public void MoveXGrounded(PlayerStateMachine stateMachine, PlayerMovementController playerController, float moveFactor = 1f) {
            MoveX(stateMachine, playerController, ACCELERATION_TIME_GROUNDED, moveFactor);
        }

        public void MoveXAirborne(PlayerStateMachine stateMachine, PlayerMovementController playerController, float moveFactor = MIN_MOVE_FACTOR_AIR) {
            MoveX(stateMachine, playerController, ACCELERATION_TIME_AIRBORNE, moveFactor);
        }

        private void MoveX(PlayerStateMachine stateMachine, PlayerMovementController playerController, float accelerationTime, float moveFactor) {
            float directionX = Input.GetAxisRaw("Horizontal");
            stateMachine.FlipSprite(directionX);
            playerController.OnMoving(directionX, accelerationTime, moveFactor);
        }
    }
}