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
        playerStateMachine = GetComponent<PlayerStateMachine>();
        spriteFlashingEffect = new SpriteFlashing(spriteRenderer);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    // Interfaces for external Interaction
    public void ReceiveDamage(float directionHitX) {
        playerStateMachine.disableUserInput = true;
        StartCoroutine(spriteFlashingEffect.Flash(4f, 0.15f));
        StartCoroutine(MoveOnDamage(directionHitX));
    }

    public IEnumerator MoveOnDamage(float directionHitX) {
        float time = Time.time;
        while( (time + 0.1f) > Time.time) {
            Debug.Log("MOVE HIT!");
            playerMovement.OnMoving(-directionHitX, 0f, 2f); // Push back in oposite direction      
            yield return new WaitForEndOfFrame();            
        }
        playerStateMachine.disableUserInput = false;
    }
}
