using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyStates;

public class EnemyController : EnemyStateMachine {

    public Object bloodSplatterParticleSystem;

    // Use this for initialization
    public override void Start() {
        base.Start();
    }

    public override Object GetBloodSplatterParticleSystem() {
        return bloodSplatterParticleSystem;
    }
}
