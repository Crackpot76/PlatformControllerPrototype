using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public class DamageState: AbstractState {

        private bool animationHasStopped;

        public override void OnEnter(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            base.OnEnter(stateMachine, animator, playerController);
            animationHasStopped = false;
            animator.SetBool(AnimEnemyParameters.DAMAGE, true);
            SoundManager.PlaySFX(stateMachine.sounds.damage);
        }

        public override AbstractState HandleUpdate(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            AbstractState baseState = base.HandleUpdate(stateMachine, animator, playerController);
            if (baseState != null) {
                return baseState;
            }

            if (animationHasStopped) {
                return stateMachine.idleState;
            }

            return null;
        }

        public override void OnExit(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimEnemyParameters.DAMAGE, false);
        }

        public override void OnAnimEvent(EnemyStateMachine stateMachine, string parameter) {
            base.OnAnimEvent(stateMachine, parameter);
            animationHasStopped = true;
        }
    }
}