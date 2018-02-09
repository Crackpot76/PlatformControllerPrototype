using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlashing {

    SpriteRenderer spriteRenderer;
    Shader shaderFlash;
    Shader shaderDefault;
    
    bool toWhite = true;


    public SpriteFlashing(SpriteRenderer spriteRenderer) {
        this.spriteRenderer = spriteRenderer;
        this.shaderFlash = Shader.Find("GUI/Text Shader"); 
        this.shaderDefault = Shader.Find("Sprites/Default");
    }


    void whiteSprite() {
        spriteRenderer.material.shader = shaderFlash;
        spriteRenderer.color = Color.white;
    }
    void normalSprite() {
        spriteRenderer.material.shader = shaderDefault;
        spriteRenderer.color = Color.white;
    }


    public IEnumerator Flash(float flashCount, float flashInterval) {
        for (int i = 0; i < flashCount; i++) {
            // switch color
            if (toWhite) {
                whiteSprite();
                toWhite = false;
            } else {
                normalSprite();
                toWhite = true;
            }
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
