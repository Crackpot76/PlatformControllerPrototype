using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

[RequireComponent (typeof (CharacterMovementController), (typeof(SpriteRenderer)))]
public class PlayerController : MonoBehaviour {
    // Central Player Controller

    CharacterMovementController playerMovement;
    SpriteRenderer spriteRenderer;
    PlayerStateMachine playerStateMachine;

    // Effects
    SpriteFlashing spriteFlashingEffect;



    // Use this for initialization
    void Start () {
        playerMovement = GetComponent<CharacterMovementController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteFlashingEffect = new SpriteFlashing(spriteRenderer);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    // Interfaces for external Interaction
    public void ReceiveDamage(float directionHitX) {
        StartCoroutine(spriteFlashingEffect.Flash(4f, 0.15f));
        playerMovement.OnMoving(-directionHitX, 0f, 7f); // Push back in oposite direction      
    }
}
