using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager: MonoBehaviour {

    // Static instance
    static EffectManager _instance;

    private FadeOutEffect fadeOutEffect;
    private SpriteFlashingEffect flashingEffect;

    public static EffectManager GetInstance() {
        if (!_instance) {
            GameObject effectManager = new GameObject("EffectManager");
            _instance = effectManager.AddComponent<EffectManager>();
            _instance.Initialize();
        }
        return _instance;
    }

    private void Initialize() {
        fadeOutEffect = new FadeOutEffect();
        flashingEffect = new SpriteFlashingEffect();
    }

    // Flashes sprite in red to indicate hit
    public void DamageFlashingSpriteEnemy(SpriteRenderer spriteRenderer) {
        StartCoroutine(flashingEffect.DamageFlashingEnemy(spriteRenderer));
    }
    // Flashes sprite in white and red to indicate hit
    public void DamageFlashingSpritePlayer(SpriteRenderer spriteRenderer) {
        StartCoroutine(flashingEffect.DamageFlashingPlayer(spriteRenderer));
    }

    // Fades out a sprite, starts with a delay in seconds (optional) and destroy gameobject when finished (optional)
    public void FadeOutSprite(SpriteRenderer spriteRenderer, float duration, float delay = 0f, bool destroy = false) {
        StartCoroutine(fadeOutEffect.FadeOut(spriteRenderer, duration, delay));
        if (destroy) {
            StartCoroutine(DestroyGameObject(spriteRenderer.gameObject, (duration + delay)));
        }
    }
    
    private IEnumerator DestroyGameObject(GameObject gameObject, float waitSeconds) {
        yield return new WaitForSeconds(waitSeconds);
        if (gameObject) {
            Destroy(gameObject);
        } else {
            Debug.LogError("EffectManager: GameObject to destroy is null!");
        }
    }
}
