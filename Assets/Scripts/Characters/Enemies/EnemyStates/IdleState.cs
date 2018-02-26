using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public class IdleState: AbstractState {
        
        public override void OnEnter(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            base.OnEnter(stateMachine, animator, playerController);
            animator.SetBool(AnimEnemyParameters.IDLE, true);
        }

        public override AbstractState HandleUpdate(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            AbstractState baseState = base.HandleUpdate(stateMachine, animator, playerController);
            if (baseState != null) {
                return baseState;
            }

            if (stateMachine.currentDetection.distance >= 0 || stateMachine.waypointController.HasWaypoints()) {                
                return stateMachine.runningState;
            }

            return null;
        }

        public override void OnExit(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimEnemyParameters.IDLE, false);
        }
        
    }
}
