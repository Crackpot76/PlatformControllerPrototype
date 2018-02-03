﻿using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class FallingState: IStateInterface {

        const float accelerationTime = 0.1f;

        public void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController) {
            animator.SetBool(AnimPlayerParamters.FALLING, true);
            Move(stateMachine, ref playerController);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController) {
                        
            // Move while jumping
            Move(stateMachine, ref playerController);

            if (playerController.IsGrounded()) {
                return PlayerStateMachine.landingIdleState;
            }
            
            return null;
        }

        private void Move(PlayerStateMachine stateMachine, ref PlayerController playerController) {
            float directionX = Input.GetAxisRaw("Horizontal");
            stateMachine.FlipSprite(directionX);
            playerController.OnMoving(directionX, accelerationTime, 1);
        }

        public void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController) {
            animator.SetBool(AnimPlayerParamters.FALLING, false);
        }

        public void OnAnimEvent(string parameter) {
            // not implemented
        }
    }
}