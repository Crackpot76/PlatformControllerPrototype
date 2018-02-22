using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyStates;

public class EnemyController : AbstractCharacterController {


    public Object bloodSplatterParticleSystem;

    // Default Werte aus AttackDetails für Bodycheck verwenden
    private AttackDetails bodyCheck = new AttackDetails();

    // Use this for initialization
    public override void Start() {
        base.Start();
    }

    public override Object GetBloodSplatterParticleSystem() {
        return bloodSplatterParticleSystem;
    }


    void OnCollisionEnter2D(Collision2D collision) {

        // check for collision with an opponent
        if (AttackController.ListContainsTag(opponentTags, collision.gameObject.tag)) {
            float hitDirectionX = AttackController.GetHitDirection(transform, collision);

            AbstractCharacterController opponentCharacterController = collision.gameObject.GetComponent<AbstractCharacterController>();

            if (opponentCharacterController) {
                opponentCharacterController.ReceiveDamage(hitDirectionX, -1, bodyCheck);
            } else {
                Debug.LogError("No PlayerController Script for player found. No damage served today!");
            }
        }
    }
}
