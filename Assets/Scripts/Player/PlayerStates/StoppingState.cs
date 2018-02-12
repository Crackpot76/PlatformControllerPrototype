using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class StoppingState : AbstractState {

        private const string DUST_EFFECT_PREFAB_NAME = "DustStoppingGO";

        private Object dustEffect;
        private bool animationHasStopped;

        public StoppingState() {
            //Init Effect Prefab
            dustEffect = Resources.Load(DUST_EFFECT_PREFAB_NAME);
        }

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParameters.STOPPING, true);
            stateMachine.InstantiateEffect(dustEffect);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController)
        {
            if (animationHasStopped) {
        
                float directionX = Input.GetAxisRaw("Horizontal");
                if (directionX == 0) {
                   return PlayerStateMachine.idleState;
                } else {
                    return PlayerStateMachine.runningState;
                }
            }
            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.STOPPING, false);
        }

        public override void OnAnimEvent(PlayerStateMachine stateMachine, string parameter) {
            animationHasStopped = true;
        }
    }
}