using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class LandIdleState: AbstractState {

        private const string DUST_EFFECT_PREFAB_NAME = "DustLandingGo";

        private Object dustEffect;
        private bool animationHasStopped;

        public LandIdleState() {
            //Init Effect Prefab
            dustEffect = Resources.Load(DUST_EFFECT_PREFAB_NAME);
        }

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParameters.LAND_IDLE, true);
            stateMachine.InstantiateEffect(dustEffect);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {

            if (Input.GetKeyDown(KeyCode.Space)) {
                return PlayerStateMachine.preJumpIdleState;
            }
            if (animationHasStopped) {
                return PlayerStateMachine.idleState;
            }

            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.LAND_IDLE, false);
        }

        public override void OnAnimEvent(PlayerStateMachine stateMachine, string parameter) {
            animationHasStopped = true;
        }
        
    }
}