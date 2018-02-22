using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public class DeathState: AbstractState {

        public override void OnEnter(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            base.OnEnter(stateMachine, animator, playerController);
            animator.SetBool(AnimEnemyParameters.DEATH, true);
        }

        public override AbstractState HandleUpdate(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            return null;
        }

        public override void OnExit(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimEnemyParameters.DEATH, false);
        }

        public override void OnAnimEvent(EnemyStateMachine stateMachine, string parameter) {
            base.OnAnimEvent(stateMachine, parameter);
            // stateMachine -> DEAD
            stateMachine.Destroy();
        }
    }
}