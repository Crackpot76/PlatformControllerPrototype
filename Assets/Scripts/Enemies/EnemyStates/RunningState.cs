using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public class RunningState: AbstractState {


        public override void OnEnter(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController, ObserverController.PlayerDetectionInfo playerDetection) {
            animator.SetBool(AnimEnemyParameters.RUNNING, true);
        }

        public override AbstractState HandleUpdate(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController, ObserverController.PlayerDetectionInfo playerDetection) {

            if (playerDetection.distance < 0) {
                //   Debug.Log("Player detected: " + playerDetection.distance);
                return EnemyStateMachine.idleState;
            }

            // run towards player
            float directionX = (playerDetection.right ? 1 : -1);

            MoveX(stateMachine, playerController, directionX);

            return null;
        }

        public override void OnExit(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController, ObserverController.PlayerDetectionInfo playerDetection) {
            animator.SetBool(AnimEnemyParameters.RUNNING, false);
        }        
    }
}
