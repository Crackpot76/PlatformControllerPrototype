using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovementController), typeof(SpriteRenderer), typeof(HealthController))]
public abstract class AbstractCharacterController : MonoBehaviour {

    // which tags can be attacked
    public string[] opponentTags;

    [HideInInspector]
    public CharacterMovementController movementController;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public HealthController health;
    [HideInInspector]
    public bool disableStateMovement = false;

    // Effects
    private SpriteFlashing spriteFlashingEffect;

    // Get the parameters of the current attack
    public abstract float GetCurrentAttackDetails();


    // Use this for initialization
    public virtual void Start() {
        movementController = GetComponent<CharacterMovementController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = GetComponent<HealthController>();
        health.RefreshDisplay();

        spriteFlashingEffect = new SpriteFlashing(spriteRenderer);

    }


    void OnCollisionEnter2D(Collision2D collision) {

        foreach (string opponentTag in opponentTags) {
            if (collision.gameObject.tag == opponentTag) {
                float hitDirectionX = AttackController.GetHitDirection(transform, collision);

                AbstractCharacterController opponentCharacterController = collision.gameObject.GetComponent<AbstractCharacterController>();

                if (opponentCharacterController) {
                    opponentCharacterController.ReceiveDamage(hitDirectionX, 1f);
                } else {
                    Debug.LogError("No PlayerController Script for player found. No damage served today!");
                }
            }
        }
    }




    // Interfaces for external Interaction
    public virtual void ReceiveDamage(float directionHitX, float damage) {
        disableStateMovement = true;
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
            movementController.OnMoving(-directionHitX, 0f, 2f); // Push back in oposite direction      
            yield return new WaitForEndOfFrame();
        }
        disableStateMovement = false;
    }
}
