using UnityEngine;
using System.Collections;

public class PlayerMovementController: MovementController {

    public float maxMoveSpeed = 7;
    public float minMoveSpeed = 5;
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    public float comicFallFactor = 1.08f;

    // calculated move variables
    float gravity;
    
    Vector3 velocity;
    float velocityXSmoothing;

    // move variables for next update
    float targetVelocityX;
    float targetVelocityY;
    float accelerationTime; 

    // SpriteRenderer 
    SpriteRenderer spriteRenderer;

    //TODO Wohl falsch hier aufgehoben
    // Effects
    SpriteFlashing spriteFlashingEffect;



    public override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Effect initialisation
        spriteFlashingEffect = new SpriteFlashing(spriteRenderer);        

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
    }



    void Update() {
        spriteFlashingEffect.Update();

        CalculateVelocity();

        Move(velocity * Time.deltaTime);

        if (collisions.above || collisions.below) {
            if (collisions.slidingDownMaxSlope) {
                velocity.y += collisions.slopeNormal.y * -gravity * Time.deltaTime;
            } else {
                velocity.y = 0;
            }
        }
        targetVelocityY = 0;
        targetVelocityX = 0;
    }


    // Interfaces for external Interaction
    public void ReceiveDamage(float directionHitX) {        
        Debug.Log("Taking Damage!");
        spriteFlashingEffect.StartFlashing(.3f);
        OnMoving(-directionHitX, 0f, 1); // Push back in oposite direction       
    }


    // --------------------------------------------------------------------------
    //  Player Controller Commands for Input
    // --------------------------------------------------------------------------

    // if bool = false, jump is not possible
    public bool OnJumping(float jumpForcePercent) {
        if (IsJumpingPossible()) {
            float possibleJumpingHeightRange = maxJumpHeight - minJumpHeight;
            float newJumpingHeight = minJumpHeight + possibleJumpingHeightRange * jumpForcePercent;

            Debug.Log(newJumpingHeight);

            float calculatedGravity = -(2 * newJumpingHeight) / Mathf.Pow(timeToJumpApex, 2);

            targetVelocityY = Mathf.Abs(calculatedGravity) * timeToJumpApex;
            return true;
        } else {
            return false;
        }        
    }

    public void OnMoving(float directionX, float accelerationTime, float moveAddonSpeedPercent) {

        float possibleMovementRange = maxMoveSpeed - minMoveSpeed;
        float newMoveSpeed = minMoveSpeed + possibleMovementRange * moveAddonSpeedPercent;

        this.targetVelocityX += directionX * newMoveSpeed;
        this.accelerationTime = accelerationTime;
    }



    // --------------------------------------------------------------------------
    // State checker
    // --------------------------------------------------------------------------
    public bool IsGrounded() {
        if (collisions.below) {
            return true;
        } else {
            return false;
        }
    }
    public bool IsFalling() {
        if (velocity.y < 0 && !collisions.below) {
            return true;
        } else {
            return false;
        }
    }

    public bool IsJumpingPossible() {
        if (collisions.below) {
            if (!collisions.slidingDownMaxSlope) {  // no jumping while sliding down                    
                return true;
            }
        }
        return false;
    }

    public bool IsSlidingSlope() {
        if (collisions.slidingDownMaxSlope) {
            return true;
        } else {
            return false;
        }
    }



    void CalculateVelocity() {

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTime);
        
        velocity.y += targetVelocityY + gravity * Time.deltaTime;

        // hier schnelleres Fallen an velocity.y manipulieren.
        if (IsFalling()) {
            velocity.y *= comicFallFactor;
        }
    }
}
