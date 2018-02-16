using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public class RunningState: AbstractState {


        public override void OnEnter(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimEnemyParameters.RUNNING, true);
        }

        public override AbstractState HandleUpdate(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            if (stateMachine.currentDetection.distance < 0) {
                //   Debug.Log("Player detected: " + playerDetection.distance);
                return EnemyStateMachine.idleState;
            }

            if (stateMachine.InAttackPosition()) {
                return EnemyStateMachine.attackIdleState;
            }

            // run towards player
            float directionX = (stateMachine.currentDetection.right ? 1 : -1);

            MoveX(stateMachine, playerController, directionX);

            return null;
        }

        public override void OnExit(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimEnemyParameters.RUNNING, false);
        }        
    }
}
