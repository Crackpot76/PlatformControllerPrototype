using UnityEngine;
using System.Collections;

public class PlayerMovementController: MovementController {
    
    public float moveSpeed = 5;
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
    


    public override void Start() {
        base.Start();  

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
    }
    

    void Update() {

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

    
    // --------------------------------------------------------------------------
    //  Player Controller Commands for Input
    // --------------------------------------------------------------------------

    // if bool = false, jump is not possible
    public bool OnJumping(float jumpForcePercent) {
        if (IsJumpingPossible()) {
            float possibleJumpingHeightRange = maxJumpHeight - minJumpHeight;
            float newJumpingHeight = minJumpHeight + possibleJumpingHeightRange * jumpForcePercent;            
            float calculatedGravity = -(2 * newJumpingHeight) / Mathf.Pow(timeToJumpApex, 2);

            targetVelocityY = Mathf.Abs(calculatedGravity) * timeToJumpApex;
            return true;
        } else {
            return false;
        }        
    }

    public void OnMoving(float directionX, float accelerationTime) {
        OnMoving(directionX, accelerationTime, 1f);
    }

    public void OnMoving(float directionX, float accelerationTime, float moveMultiplier) {

        this.targetVelocityX += directionX * moveSpeed * moveMultiplier;
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
