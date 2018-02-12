using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class LandRunningState: AbstractState {

        private const string DUST_EFFECT_PREFAB_NAME = "DustLandingDirectionGO";

        private Object dustEffect;
        private bool animationHasStopped;

        public LandRunningState() {
            //Init Effect Prefab
            dustEffect = Resources.Load(DUST_EFFECT_PREFAB_NAME);
        }

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParameters.LAND_RUNNING, true);
            stateMachine.InstantiateEffect(dustEffect);
            MoveXGrounded(stateMachine, playerController);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            
            if (animationHasStopped) {
                float directionX = Input.GetAxisRaw("Horizontal");

                if (directionX == 0) {
                    return PlayerStateMachine.stoppingState;
                } else {
                    return PlayerStateMachine.runningState;
                }                
            }

            MoveXGrounded(stateMachine, playerController);

            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.LAND_RUNNING, false);
        }

        public override void OnAnimEvent(PlayerStateMachine stateMachine, string parameter) {
            animationHasStopped = true;
        }
    }
}