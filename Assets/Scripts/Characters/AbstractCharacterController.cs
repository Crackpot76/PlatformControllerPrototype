using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovementController), typeof(SpriteRenderer), typeof(HealthController))]
public abstract class AbstractCharacterController : MonoBehaviour {

    // which opponents with tags can be attacked
    public string[] opponentTags;
    // which objects with tags can be destroyed
    public string[] destructableTags;


    [HideInInspector]
    public CharacterMovementController movementController;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public HealthController health;
    [HideInInspector]
    public bool disableStateMovement = false;

    // Default Werte aus AttackDetails für Bodycheck verwenden
    private AttackDetails bodyCheck = new AttackDetails();

    // Effects
    private SpriteFlashing spriteFlashingEffect;

    // Get the parameters of the current attack
    public abstract AttackDetails GetCurrentAttackDetails();


    // Use this for initialization
    public virtual void Start() {
        movementController = GetComponent<CharacterMovementController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = GetComponent<HealthController>();
        health.RefreshDisplay();

        spriteFlashingEffect = new SpriteFlashing(spriteRenderer);

    }


    void OnCollisionEnter2D(Collision2D collision) {

        // check for collision with an opponent
        if (AttackController.ListContainsTag(opponentTags, collision.gameObject.tag)) {
            float hitDirectionX = AttackController.GetHitDirection(transform, collision);

            AbstractCharacterController opponentCharacterController = collision.gameObject.GetComponent<AbstractCharacterController>();

            if (opponentCharacterController) {
                opponentCharacterController.ReceiveDamage(hitDirectionX, bodyCheck);
            } else {
                Debug.LogError("No PlayerController Script for player found. No damage served today!");
            }
        }
    }



    // Interfaces for external Interaction
    public virtual void ReceiveDamage(float directionHitX, AttackDetails attack) {
        disableStateMovement = true;
        StartCoroutine(spriteFlashingEffect.Flash(4f, 0.15f));        

        health.TakeDamage(attack.damage);
        if (attack.pushOnDamage) {
            StartCoroutine(MoveOnDamage(directionHitX, attack.pushSpeed));
        }
        if (!health.IsAlive()) {
            // GAME OVER
            Destroy(gameObject);
        } else {
            Invoke("EnableStateMovement", .1f);
        }
    }

    public IEnumerator MoveOnDamage(float directionHitX, float pushSpeed) {
        float time = Time.time;
        while ((time + 0.1f) > Time.time) {
            movementController.OnPushed(directionHitX, pushSpeed); // Push back in oposite direction      
            yield return new WaitForEndOfFrame();
        }
    }


    // Enables state movement after certain time
    void EnableStateMovement() {
        disableStateMovement = false;
    }
}
