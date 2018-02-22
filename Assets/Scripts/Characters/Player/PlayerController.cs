using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

public class PlayerController : AbstractCharacterController {

    private Object bloodSplatterParticleSystem;

    // Use this for initialization
    public override void Start() {
        base.Start();
        bloodSplatterParticleSystem = Resources.Load("BloodHit");
    }

    public override Object GetBloodSplatterParticleSystem() {
        return bloodSplatterParticleSystem;
    }

}
