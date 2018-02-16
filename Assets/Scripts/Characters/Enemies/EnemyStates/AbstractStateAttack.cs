using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public abstract class AbstractStateAttack: AbstractState {

        public abstract AttackDetails GetAttackDetails();
    }
}
