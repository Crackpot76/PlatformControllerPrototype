using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public class RunningState: AbstractState {

        
        public override void OnEnter(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            base.OnEnter(stateMachine, animator, playerController);
            animator.SetBool(AnimEnemyParameters.RUNNING, true);
         }

        public override AbstractState HandleUpdate(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            AbstractState baseState = base.HandleUpdate(stateMachine, animator, playerController);
            if (baseState != null) {
                return baseState;
            }

            float directionX = 0;

            if (stateMachine.currentDetection.distance < 0) {
                // Kein Gegner gefunden

                if (stateMachine.waypointController.HasWaypoints()) {
                    // run towards waypoint
                    directionX = stateMachine.waypointController.GetDirectionXnextWaypoint(stateMachine.transform.position);
                } else {
                    return stateMachine.idleState;
                }
            } else {
                // Gegen gefunden

                if (stateMachine.InAttackPosition()) {
                    return stateMachine.attackIdleState;
                } else {
                    // run towards player
                    directionX = (stateMachine.currentDetection.right ? 1 : -1);
                }
            }


            MoveX(stateMachine, playerController, directionX);

            return null;
        }

        public override void OnExit(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimEnemyParameters.RUNNING, false);
        }

    }
}
