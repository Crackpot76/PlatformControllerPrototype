using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates {
    public abstract class AbstractStateAttack: AbstractState {

        public abstract AttackDetails GetAttackDetails();
        
    }
}
