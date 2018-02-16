using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

public class PlayerController : PlayerStateMachine {

    // Use this for initialization
    public override void Start() {
        base.Start();
    }



    // Interfaces for external Interaction
    public override void ReceiveDamage(float directionHitX, float damage) {
        base.ReceiveDamage(directionHitX, damage);
        // hier kann noch mehr individuell passieren, wenn gewünscht
    }
    
}
