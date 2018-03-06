using UnityEngine;
using System.Collections;

namespace PlayerStates {
    public abstract class AbstractStateAir : AbstractState {

        public float initialRunJumpDirectionX = 0f;
        public float moveMultiplierAir = MIN_MOVE_FACTOR_AIR;

        public void ResetStateAir() {
            initialRunJumpDirectionX = 0f;
            moveMultiplierAir = MIN_MOVE_FACTOR_AIR;
        }

        public void InitParameters(float runJumpDirectionX, float moveMultiplierAir = MIN_MOVE_FACTOR_AIR) {
            this.initialRunJumpDirectionX = runJumpDirectionX;
            this.moveMultiplierAir = moveMultiplierAir;
        }

        public void Move(PlayerStateMachine stateMachine, CharacterMovementController playerController) {
            if (initialRunJumpDirectionX != Input.GetAxisRaw("Horizontal")) {
                moveMultiplierAir = MIN_MOVE_FACTOR_AIR; // wieder initialisieren da Initialrichtung gewechselt
            }
            MoveXAirborne(stateMachine, playerController, moveMultiplierAir);
        }
    }
}