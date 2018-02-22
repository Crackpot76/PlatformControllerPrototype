using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public class AttackIdleState: AbstractState {

        private const float minAttackInterval = 0.5f;
        private const float maxAttackInterval = 0.8f;

        private float lastAttackTime;
        private bool damaged = false;

        public override void OnEnter(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimEnemyParameters.IDLE, true);
            lastAttackTime = Time.time;
            damaged = false;
        }

        public override AbstractState HandleUpdate(EnemyStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            if (damaged) {
                return EnemyStateMachine.damageState;
            }

            if (!stateMachine.InAttackPosition()) {
                //  Player out of reach
                return EnemyStateMachine.runningState;
            }

            if (IsTimeToAttack(stateMachine)) {
                return EnemyStateMachine.attackingState;
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



        bool IsTimeToAttack(EnemyStateMachine stateMachine) {
            float meanAttackTime = stateMachine.attackEverySeconds;
            float attacksPerSecond = 1 / meanAttackTime;

            if (Time.deltaTime > meanAttackTime) {
                Debug.LogWarning("Attackrate capped by frame rate!");
            }

            float threshold = attacksPerSecond * Time.deltaTime;
            float random = Random.value;

            bool result = false;
            if (random < threshold) {
                if ((Time.time - lastAttackTime) > minAttackInterval) {
                    result = true;
                }                
            } else {
                if ((Time.time - lastAttackTime) > maxAttackInterval) {
                    result = true;
                }
            }

            if (result) {
                lastAttackTime = Time.time;
            }
            
            return result;
        }
    }
}
