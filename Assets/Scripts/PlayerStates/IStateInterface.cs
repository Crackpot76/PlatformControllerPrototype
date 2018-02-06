using UnityEngine;
using System.Collections;

namespace PlayerStates {
   interface IStateInterface {

       void OnEnter(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController);
       IStateInterface HandleUpdate(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController);
       void OnExit(PlayerStateMachine stateMachine, Animator animator, PlayerMovementController playerController);
        void OnAnimEvent(string parameter);
   }
}