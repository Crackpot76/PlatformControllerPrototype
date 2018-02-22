using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlashing {

    SpriteRenderer spriteRenderer;
    Shader shaderFlash;
    Shader shaderDefault;


    public SpriteFlashing(SpriteRenderer spriteRenderer) {
        this.spriteRenderer = spriteRenderer;
        this.shaderFlash = Shader.Find("GUI/Text Shader"); 
        this.shaderDefault = Shader.Find("Sprites/Default");
    }


    void whiteSprite() {
        spriteRenderer.material.shader = shaderFlash;
        spriteRenderer.color = Color.white;
    }
    void redSprite() {
        spriteRenderer.material.shader = shaderDefault;
        spriteRenderer.color = Color.red;
    }
    void invisibleSprite() {
        spriteRenderer.material.shader = shaderDefault;
        spriteRenderer.color = Color.white;
        Color c = spriteRenderer.color;
        c.a = 0f;
        spriteRenderer.color = c;
    }
    void normalSprite() {
        spriteRenderer.material.shader = shaderDefault;
        spriteRenderer.color = Color.white;
    }

    public IEnumerator Flash(float flashCount, float flashInterval) {
        for (int i = 0; i < 4; i++) {
            // switch color
            if (i==0) {
                whiteSprite();
            }
            if (i == 1) {
                redSprite();
            }
            if (i == 2) {
                invisibleSprite();
            }
            if (i == 3) {
                normalSprite();
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
