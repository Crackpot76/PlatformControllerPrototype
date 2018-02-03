using UnityEngine;
using System.Collections;

namespace PlayerStates {
   interface IStateInterface {

       void OnEnter(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController);
       IStateInterface HandleUpdate(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController);
       void OnExit(PlayerStateMachine stateMachine, ref Animator animator, ref PlayerController playerController);
        void OnAnimEvent(string parameter);
   }
}