using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public class IdleState: AbstractState {

        private bool damaged = false;

        public override void OnEnter(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimEnemyParameters.IDLE, true);
            damaged = false;
    }

        public override AbstractState HandleUpdate(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            if (damaged) {
                return EnemyStateMachine.damageState;
            }
            if (stateMachine.currentDetection.distance >= 0) {
                //   Debug.Log("Player detected: " + playerDetection.distance);
                return EnemyStateMachine.runningState;
            }

            return null;
        }

        public override void OnExit(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimEnemyParameters.IDLE, false);
        }

        public override void OnAnimEvent(EnemyStateMachine stateMachine, string parameter) {
            if (parameter.Equals("DAMAGE")) {
                damaged = true;
            }
        }
    }
}
