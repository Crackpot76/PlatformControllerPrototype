﻿using UnityEngine;
using System.Collections;

namespace PlayerStates {
    class JumpStartRunningState: IStateInterface {

        const float accelerationTime = 0.1f;
        bool animationHasStopped;
        float runJumpDirectionX = 0f;        
        float moveMultiplierAir = 2f;

        public void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {
            animationHasStopped = false;
            animator.SetBool(AnimPlayerParamters.JUMP_START_RUNNING, true);
            Move(stateMachine, ref playerController);
        }

        public IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {

            if (playerController.IsGrounded()) {
                // Debug.Log("Allready grounded!!!");
            }

            if (animationHasStopped) {
                PlayerStateMachine.jumpAirState.InitParameter(runJumpDirectionX, moveMultiplierAir);
                return PlayerStateMachine.jumpAirState;
            }

            // Move while jumping
            Move(stateMachine, ref playerController);
            return null;
        }

        private void Move(PlayerStateMachine stateMachine, ref PlayerMovementController playerController) {

            playerController.OnMoving(runJumpDirectionX, accelerationTime, moveMultiplierAir);
        }

        public void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController) {
            animator.SetBool(AnimPlayerParamters.JUMP_START_RUNNING, false);
            runJumpDirectionX = 0f;
        }

        public void OnAnimEvent(string parameter) {
            animationHasStopped = true;
        }

        public void InitParameters(float runJumpDirectionX) {
            this.runJumpDirectionX = runJumpDirectionX;
        }
    }
}