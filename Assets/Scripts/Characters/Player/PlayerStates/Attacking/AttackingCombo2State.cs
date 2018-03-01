using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class AttackingCombo2State: AbstractStateAttack {

        private const string DUST_EFFECT_PREFAB_NAME = "DustStoppingGO";

        private Object dustEffect;
        private bool transitCombo;
        private bool animationHasStopped;
        private AttackDetails attack;

        public AttackingCombo2State() {
            //Init Effect Prefab
            dustEffect = Resources.Load(DUST_EFFECT_PREFAB_NAME);

            // Define Attack Details
            attack = new AttackDetails(); // default attack
            attack.type = AttackDetails.AttackType.Sharp;
            attack.pushOnDamage = false; // light attack no push
            attack.criticalHitPercent = 0.5f; // 50% crit
        }

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            Debug.Log("IN COMBO2");
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParameters.ATTACKING_COMBO2, true);
            SoundManager.PlaySFX(stateMachine.sounds.baseAttack);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            if (animationHasStopped) {
                return PlayerStateMachine.idleState;
            }
            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.ATTACKING_COMBO2, false);
        }

        public override void OnAnimEvent(PlayerStateMachine stateMachine, string parameter) {
            if (parameter.Equals("DUST_EFFECT")) {
                stateMachine.InstantiateEffect(dustEffect);
            } else {
                animationHasStopped = true;
            }
        }

        public override AttackDetails GetAttackDetails() {
            return attack;
        }
    }
}