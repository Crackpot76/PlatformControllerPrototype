using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class AttackingLightState: AbstractStateAttack {

        private const string DUST_EFFECT_PREFAB_NAME = "DustStoppingGO";

        private Object dustEffect;
        private bool animationHasStopped;
        private AttackDetails attack;

        public AttackingLightState() {
            //Init Effect Prefab
            dustEffect = Resources.Load(DUST_EFFECT_PREFAB_NAME);

            // Define Attack Details
            attack = new AttackDetails(); // default attack
            attack.type = AttackDetails.AttackType.Sharp;
            attack.pushOnDamage = false; // light attack no push
        }

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParameters.ATTACKING_LIGHT, true);
            stateMachine.InstantiateEffect(dustEffect);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            if (animationHasStopped) {
               return PlayerStateMachine.idleState;
            }
            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.ATTACKING_LIGHT, false);
        }

        public override void OnAnimEvent(PlayerStateMachine stateMachine, string parameter) {
            animationHasStopped = true;
        }

        public override AttackDetails GetAttackDetails() {
            return attack;
        }
    }
}