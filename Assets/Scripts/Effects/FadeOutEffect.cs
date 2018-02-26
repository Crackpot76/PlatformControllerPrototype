using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutEffect {
    
    private float minimum = 0.0f;
    private float maximum = 1f;
    private float startTime;
    


    public IEnumerator FadeOut(SpriteRenderer spriteRenderer, float duration, float delay) {
        yield return new WaitForSeconds(delay);
        startTime = Time.time - 0.01f;
        while (spriteRenderer != null && spriteRenderer.color.a > 0) {
            float t = (Time.time - startTime) / duration;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1 - Mathf.SmoothStep(minimum, maximum, t));
            yield return new WaitForFixedUpdate();
        }
    }
    
}
