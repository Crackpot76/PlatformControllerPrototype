using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyStates;

public class EnemyController : CharacterInterface {
    // Central Player Controller

    CharacterMovementController playerMovement;
    SpriteRenderer spriteRenderer;
    EnemyStateMachine enemyStateMachine;
    HealthController health;

    // Effects
    SpriteFlashing spriteFlashingEffect;



    // Use this for initialization
    void Start() {
        playerMovement = GetComponent<CharacterMovementController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyStateMachine = GetComponent<EnemyStateMachine>();
        health = GetComponent<HealthController>();
        health.RefreshDisplay();

        spriteFlashingEffect = new SpriteFlashing(spriteRenderer);

    }



    // Interfaces for external Interaction
    public override void ReceiveDamage(float directionHitX, float damage) {
        enemyStateMachine.disabled = true;
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
        while ((time + 0.1f) > Time.time) {
            playerMovement.OnMoving(-directionHitX, 0f, 12f); // Push back in oposite direction      
            yield return new WaitForEndOfFrame();
        }
        enemyStateMachine.disabled = false;
    }
}
