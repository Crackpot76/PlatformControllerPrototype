using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public abstract class AbstractState {
        public const float ACCELERATION_TIME_GROUNDED = 0.04f;
        public const float ACCELERATION_TIME_AIRBORNE = 0.1f;
        public const float MIN_MOVE_FACTOR_AIR = 1.5f;
        public const float MAX_MOVE_FACTOR_AIR = 2f;
        public const float MIDDLE_MOVE_FACTOR_AIR = (MAX_MOVE_FACTOR_AIR - MIN_MOVE_FACTOR_AIR) / 2 + MIN_MOVE_FACTOR_AIR;


        public abstract void OnEnter(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController, ObserverController.PlayerDetectionInfo playerDetection);
        public abstract AbstractState HandleUpdate(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController, ObserverController.PlayerDetectionInfo playerDetection);
        public abstract void OnExit(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController, ObserverController.PlayerDetectionInfo playerDetection);

        public virtual void OnAnimEvent(EnemyStateMachine stateMachine, string parameter) {
            // not implemented
        }

        public void MoveX(EnemyStateMachine stateMachine, CharacterMovementController playerController, float directionX, float accelerationTime = ACCELERATION_TIME_GROUNDED, float moveFactor = 1f) {
            stateMachine.FlipSprite(directionX);
            playerController.OnMoving(directionX, accelerationTime, moveFactor);
        }


        // Moves Player in directionX, without sprite flipping!
        public void MoveXRaw(CharacterMovementController playerController, float directionX, float accelerationTime, float moveFactor) {
            playerController.OnMoving(directionX, accelerationTime, moveFactor);
        }
    }
}
