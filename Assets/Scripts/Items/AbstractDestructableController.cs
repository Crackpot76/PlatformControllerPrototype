using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDestructableController: MonoBehaviour {

    // Interfaces for external Interaction
    public abstract void ReceiveDamage(float directionHitX, float maxHitContactY, AttackDetails attack);
    
}
