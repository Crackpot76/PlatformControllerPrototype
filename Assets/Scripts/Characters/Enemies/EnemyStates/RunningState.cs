using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public class RunningState: AbstractState {


        private bool damaged = false;

        public override void OnEnter(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimEnemyParameters.RUNNING, true);
            damaged = false;
         }

        public override AbstractState HandleUpdate(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            if (damaged) {
                return EnemyStateMachine.damageState;
            }

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

        public override void OnAnimEvent(EnemyStateMachine stateMachine, string parameter) {
            if (parameter.Equals("DAMAGE")) {
                damaged = true;
            }
        }
    }
}
