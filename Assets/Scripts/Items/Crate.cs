using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : AbstractDestructableController {
    
    public Sprite damageSprite;
    public Sprite destroyedSprite;

    public GameObject damageEffect;
    public GameObject explosionEffect;

    public AudioClip damageSound;

    private float currentHealth = 2;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;


    public override void ReceiveDamage(float directionHitX, float maxHitContactY, AttackDetails attack) {

        // nur attack Damage relevant
        if (attack != null && attack.damage > 0) {
            currentHealth -= attack.damage;

            InstantiateEffect(damageEffect);
            InstantiateEffect(explosionEffect);
            SoundManager.PlaySFX(damageSound);

            if (currentHealth <= 0) {
                // destroyed
                Debug.Log("Destroyed!");
                spriteRenderer.sprite = destroyedSprite;
                boxCollider.enabled = false;
            } else if (currentHealth < 2) {
                // damageSprite
                Debug.Log("Damage!");
                spriteRenderer.sprite = damageSprite;
            }
        }

    }

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

    }


    private void InstantiateEffect(GameObject effectGo) {
        GameObject effect = Instantiate(effectGo);


        effect.transform.parent = transform;
        effect.transform.position = transform.position;
    }
}
