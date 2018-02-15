using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

[RequireComponent (typeof (CharacterMovementController), (typeof(SpriteRenderer)))]
public class PlayerController : CharacterInterface {
    // Central Player Controller

    CharacterMovementController playerMovement;
    SpriteRenderer spriteRenderer;
    PlayerStateMachine playerStateMachine;
    HealthController health;

    // Effects
    SpriteFlashing spriteFlashingEffect;



    // Use this for initialization
    void Start () {
        playerMovement = GetComponent<CharacterMovementController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStateMachine = GetComponent<PlayerStateMachine>();
        health = GetComponent<HealthController>();
        health.RefreshDisplay();

        spriteFlashingEffect = new SpriteFlashing(spriteRenderer);
        
    }



    // Interfaces for external Interaction
    public override void ReceiveDamage(float directionHitX, float damage) {
        playerStateMachine.disableUserInput = true;
        StartCoroutine(spriteFlashingEffect.Flash(4f, 0.15f));
        StartCoroutine(MoveOnDamage(directionHitX));
        health.TakeDamage(damage);

        if (!health.IsAlive()) {
            // GAME OVER
            Destroy(gameObject);
        }
    }

    public IEnumerator MoveOnDamage(float directionHitX) {
        float time = Time.time;
        while( (time + 0.1f) > Time.time) {
            playerMovement.OnMoving(-directionHitX, 0f, 0f); // Push back in oposite direction      
            yield return new WaitForEndOfFrame();            
        }
        playerStateMachine.disableUserInput = false;
    }
}
