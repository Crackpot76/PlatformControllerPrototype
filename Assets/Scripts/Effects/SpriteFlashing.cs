using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlashing {

    SpriteRenderer spriteRenderer;
    Shader shaderFlash;
    Shader shaderDefault;

    const float defaultSwitchTimer = .1f;
    float switchTimer;
    bool toWhite = true;

    bool started = false;
    float timer;

    public SpriteFlashing(SpriteRenderer spriteRenderer) {
        this.spriteRenderer = spriteRenderer;
        this.shaderFlash = Shader.Find("GUI/Text Shader"); 
        this.shaderDefault = Shader.Find("Sprites/Default");
        switchTimer = defaultSwitchTimer;
    }


    void whiteSprite() {
        spriteRenderer.material.shader = shaderFlash;
        spriteRenderer.color = Color.white;
    }
    void normalSprite() {
        spriteRenderer.material.shader = shaderDefault;
        spriteRenderer.color = Color.white;
    }

    public void StartFlashing(float timerFlashing) {
        started = true;
        timer = timerFlashing;        
    }

    public void Update() { 
        if (started) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                normalSprite();
                toWhite = true;
                started = false;
            } else {
                UpdateShader();
            }

        }
    }

    void UpdateShader() {
        switchTimer -= Time.deltaTime;
        if (switchTimer <= 0) {
            // switch color
            if (toWhite) {
                whiteSprite();
                toWhite = false;
            } else {
                normalSprite();
                toWhite = true;
            }
            switchTimer = defaultSwitchTimer;
        }
    }

}
