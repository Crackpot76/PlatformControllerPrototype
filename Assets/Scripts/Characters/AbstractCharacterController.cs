using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovementController), typeof(SpriteRenderer), typeof(HealthController))]
public abstract class AbstractCharacterController : MonoBehaviour {

    // which opponents with tags can be attacked
    public string[] opponentTags;
    // which objects with tags can be destroyed
    public string[] destructableTags;


    protected IStateMachine stateMachineInterface;
    protected CharacterMovementController movementController;
    protected SpriteRenderer spriteRenderer;
    protected BoxCollider2D boxCollider;
    protected HealthController health;


    // Effects
    private SpriteFlashingEffect spriteFlashingEffect;
    

    public abstract Object GetBloodSplatterParticleSystem();


    // Use this for initialization
    public virtual void Start() {
        stateMachineInterface = GetComponent<IStateMachine>();
        movementController = GetComponent<CharacterMovementController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        health = GetComponent<HealthController>();
        health.RefreshDisplay();
    }
    

    // Interfaces for external Interaction
    public virtual void ReceiveDamage(float directionHitX, float maxHitContactY, AttackDetails attack) {

        if (attack.damage > 0) {

            // Blood Effect on sharp attack
            if (attack.type.Equals(AttackDetails.AttackType.Sharp)) {
                InstantiateBloodSplatterParticleSystem(directionHitX, maxHitContactY);
            }

            // Start flashing animation
            EffectManager.GetInstance().DamageFlashingSprite(spriteRenderer);

            // calculate damage on health
            health.TakeDamage(attack.damage);

            // push effect on model
            if (attack.pushOnDamage) {
                StartCoroutine(MoveOnDamage(directionHitX, attack.pushSpeed));
            }

            // what to do on when health is below zero
            if (!health.IsAlive()) {
                // GAME OVER
                stateMachineInterface.EventTrigger(EventParameters.DEATH);
                boxCollider.enabled = false;
            } else {
                stateMachineInterface.EventTrigger(EventParameters.DAMAGE);
            }
        }
    }

    private IEnumerator MoveOnDamage(float directionHitX, float pushSpeed) {
        float time = Time.time;
        while ((time + 0.1f) > Time.time) {
            movementController.OnPushed(directionHitX, pushSpeed); // Push back in oposite direction      
            yield return new WaitForEndOfFrame();
        }
    }



    private void InstantiateBloodSplatterParticleSystem(float directionHitX, float maxHitContactY) {
        GameObject bloodSplatter = (GameObject)Instantiate(GetBloodSplatterParticleSystem());
        ParticleSystem particleSystem = bloodSplatter.GetComponent<ParticleSystem>();

        if (directionHitX > 0) {
            // bloodSplatter rotate 180 degrees
            bloodSplatter.transform.rotation = new Quaternion(bloodSplatter.transform.rotation.x, 180, bloodSplatter.transform.rotation.z, bloodSplatter.transform.rotation.w);
        }
        
        bloodSplatter.transform.parent = transform;

        bloodSplatter.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
        bloodSplatter.transform.position = new Vector2(transform.position.x, maxHitContactY);

        particleSystem.Play();
    }
}
