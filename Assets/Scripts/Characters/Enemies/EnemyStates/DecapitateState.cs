using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public class DecapitateState: AbstractState {

        public override void OnEnter(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            base.OnEnter(stateMachine, animator, playerController);
            animator.SetBool(AnimEnemyParameters.DECAP, true);
            stateMachine.Decapitate();
            SoundManager.PlaySFX(stateMachine.sounds.deadCritical);
        }

        public override AbstractState HandleUpdate(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            return null;
        }

        public override void OnExit(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimEnemyParameters.DECAP, false);
        }

        public override void OnAnimEvent(EnemyStateMachine stateMachine, string parameter) {
            base.OnAnimEvent(stateMachine, parameter);
            // stateMachine -> DEAD
            stateMachine.Destroy();
        }
    }
}