using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public class IdleState: AbstractState {


        public override void OnEnter(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController, ObserverController.PlayerDetectionInfo playerDetection) {
            animator.SetBool(AnimEnemyParameters.IDLE, true);
        }

        public override AbstractState HandleUpdate(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController, ObserverController.PlayerDetectionInfo playerDetection) {

            if (playerDetection.distance >= 0) {
                //   Debug.Log("Player detected: " + playerDetection.distance);
                return EnemyStateMachine.runningState;
            }

            return null;
        }

        public override void OnExit(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController, ObserverController.PlayerDetectionInfo playerDetection) {
            animator.SetBool(AnimEnemyParameters.IDLE, false);
        }        
    }
}
