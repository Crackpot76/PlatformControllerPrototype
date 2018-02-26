using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlashingEffect {
    
    Shader shaderFlash;
    Shader shaderDefault;


    public SpriteFlashingEffect() {
        this.shaderFlash = Shader.Find("GUI/Text Shader"); 
        this.shaderDefault = Shader.Find("Sprites/Default");
    }


    void whiteSprite(SpriteRenderer spriteRenderer) {
        spriteRenderer.material.shader = shaderFlash;
        spriteRenderer.color = Color.white;
    }
    void redSprite(SpriteRenderer spriteRenderer) {
        spriteRenderer.material.shader = shaderDefault;
        spriteRenderer.color = Color.red;
    }
    void invisibleSprite(SpriteRenderer spriteRenderer) {
        spriteRenderer.material.shader = shaderDefault;
        spriteRenderer.color = Color.white;
        Color c = spriteRenderer.color;
        c.a = 0f;
        spriteRenderer.color = c;
    }
    void normalSprite(SpriteRenderer spriteRenderer) {
        spriteRenderer.material.shader = shaderDefault;
        spriteRenderer.color = Color.white;
    }

    public IEnumerator DamageFlashingPlayer(SpriteRenderer spriteRenderer) {
        for (int i = 0; i < 4; i++) {
            // switch color
            if (i==0) {
                whiteSprite(spriteRenderer);
            }
            if (i == 1) {
                redSprite(spriteRenderer);
            }
            if (i == 2) {
                invisibleSprite(spriteRenderer);
            }
            if (i == 3) {
                normalSprite(spriteRenderer);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator DamageFlashingEnemy(SpriteRenderer spriteRenderer) {
        for (int i = 0; i < 3; i++) {
            // switch color
            if (i == 0) {
                redSprite(spriteRenderer);
            }
            if (i == 1) {
                invisibleSprite(spriteRenderer);
            }
            if (i == 2) {
                normalSprite(spriteRenderer);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
