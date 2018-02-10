using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerMovementController))]
public class PlayerController : MonoBehaviour {
    // Central Player Controller

    PlayerMovementController playerMovement;
    SpriteRenderer spriteRenderer;

    // Effects
    SpriteFlashing spriteFlashingEffect;

    public AudioClip audioClip;

    // Use this for initialization
    void Start () {
        playerMovement = GetComponent<PlayerMovementController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteFlashingEffect = new SpriteFlashing(spriteRenderer);

        SoundManager.SetBGMVolume(0.5f);
        SoundManager.PlayBGM(audioClip, false, 0);



    }
	
	// Update is called once per frame
	void Update () {
		
	}


    // Interfaces for external Interaction
    public void ReceiveDamage(float directionHitX) {
        StartCoroutine(spriteFlashingEffect.Flash(4f, 0.15f));
        playerMovement.OnMoving(-directionHitX, 0f, 5f); // Push back in oposite direction      
    }
}
