using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public class DamageState: AbstractState {

        private bool animationHasStopped;

        public override void OnEnter(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimEnemyParameters.DAMAGE, true);
        }

        public override AbstractState HandleUpdate(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            if (animationHasStopped) {
                return EnemyStateMachine.idleState;
            }

            return null;
        }

        public override void OnExit(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimEnemyParameters.DAMAGE, false);
        }

        public override void OnAnimEvent(EnemyStateMachine stateMachine, string parameter) {
            animationHasStopped = true;
        }
    }
}