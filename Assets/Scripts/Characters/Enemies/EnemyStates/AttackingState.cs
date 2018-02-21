using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public class AttackingState: AbstractStateAttack {

        private bool animationHasStopped;
        private AttackDetails attack;

        public AttackingState() {
            // Define Attack Details
            attack = new AttackDetails(); // default attack
            attack.type = AttackDetails.AttackType.Sharp;
        }

        public override void OnEnter(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimEnemyParameters.ATTACKING, true);
        }

        public override AbstractState HandleUpdate(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            if (stateMachine.damagedEvent) {
                return EnemyStateMachine.damageState;
            }

            if (animationHasStopped) {
                return EnemyStateMachine.attackIdleState;
            }
            
            return null;
        }

        public override void OnExit(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimEnemyParameters.ATTACKING, false);
        }

        public override void OnAnimEvent(EnemyStateMachine stateMachine, string parameter) {
            animationHasStopped = true;
        }

        public override AttackDetails GetAttackDetails() {
            return attack;
        }
    }
}
