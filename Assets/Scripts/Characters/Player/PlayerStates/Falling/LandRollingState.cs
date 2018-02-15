using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public class LandRollingState: AbstractState {

        private const string DUST_EFFECT_PREFAB_NAME = "DustLandingDirectionGO";

        private  Object dustEffect;
        private bool animationHasStopped;

        public LandRollingState() {
            //Init Effect Prefab
            dustEffect = Resources.Load(DUST_EFFECT_PREFAB_NAME);
        }

        public override void OnEnter(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParameters.LAND_ROLLING, true);
            stateMachine.InstantiateEffect(dustEffect);
            MoveXRaw(playerController, stateMachine.currentDirectionX, ACCELERATION_TIME_GROUNDED, 1f);
        }

        public override AbstractState HandleUpdate(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {

            if (animationHasStopped) {
                float directionX = Input.GetAxisRaw("Horizontal");

                if (directionX == 0) {
                    return PlayerStateMachine.stoppingState;
                } else {
                    return PlayerStateMachine.runningState;
                }
            }

            // Automatic rolling forward, no Input required
            MoveXRaw(playerController, stateMachine.currentDirectionX, ACCELERATION_TIME_GROUNDED, 1f);

            return null;
        }

        public override void OnExit(PlayerStateMachine stateMachine, Animator animator, CharacterMovementController playerController) {
            animator.SetBool(AnimPlayerParameters.LAND_ROLLING, false);
        }

        public override void OnAnimEvent(PlayerStateMachine stateMachine, string parameter) {
            animationHasStopped = true;
        }
    }
}