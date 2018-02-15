using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public class AttackingState: AbstractState {

        bool animationHasStopped;

        public override void OnEnter(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController, ObserverController.PlayerDetectionInfo playerDetection) {
            animationHasStopped = false;
            animator.SetBool(AnimEnemyParameters.ATTACKING, true);
        }

        public override AbstractState HandleUpdate(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController, ObserverController.PlayerDetectionInfo playerDetection) {

            if (animationHasStopped) {
                return EnemyStateMachine.attackIdleState;
            }
            
            return null;
        }

        public override void OnExit(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController, ObserverController.PlayerDetectionInfo playerDetection) {
            animator.SetBool(AnimEnemyParameters.ATTACKING, false);
        }

        public override void OnAnimEvent(EnemyStateMachine stateMachine, string parameter) {
            animationHasStopped = true;
        }
    }
}
