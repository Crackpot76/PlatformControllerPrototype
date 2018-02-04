using UnityEngine;
using System.Collections;

namespace PlayerStates {
   interface IStateInterface {

       void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController);
       IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController);
       void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerMovementController playerController);
        void OnAnimEvent(string parameter);
   }
}