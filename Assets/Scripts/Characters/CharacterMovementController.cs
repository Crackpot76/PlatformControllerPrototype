using UnityEngine;
using System.Collections;

public class CharacterMovementController: MovementController {

    
    public float moveSpeed = 5;
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    public float comicFallFactor = 1.08f;
    public float climbLadderSpeed = 5;

    // calculated move variables
    float gravity;
    
    Vector3 velocity;
    float velocityXSmoothing;

    // move variables for next update
    float targetVelocityX;
    float targetVelocityY;
    float accelerationTime;

    bool useLadder = false;    


    public override void Start() {
        base.Start();  

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
    }
    

    void Update() {

        CalculateVelocity();

        Move(velocity * Time.deltaTime, useLadder);

        if (collisions.above || collisions.below) {
            if (collisions.slidingDownMaxSlope) {
                velocity.y += collisions.slopeNormal.y * -gravity * Time.deltaTime;
            } else {
                velocity.y = 0;
            }
        }
        targetVelocityY = 0;
        targetVelocityX = 0;
        useLadder = false;
    }

    
    // --------------------------------------------------------------------------
    //  Player Controller Commands for Input
    // --------------------------------------------------------------------------

    // if bool = false, jump is not possible
    public bool OnJumping(float jumpForcePercent) {
        if (IsJumpingPossible()) {
            collisions.onLadder = false;
            float possibleJumpingHeightRange = maxJumpHeight - minJumpHeight;
            float newJumpingHeight = minJumpHeight + possibleJumpingHeightRange * jumpForcePercent;            
            float calculatedGravity = -(2 * newJumpingHeight) / Mathf.Pow(timeToJumpApex, 2);

            targetVelocityY = Mathf.Abs(calculatedGravity) * timeToJumpApex;
            return true;
        } else {
            return false;
        }        
    }

    public void OnClimbLadder(float directionY) {
        if (LadderBelow()) {
            useLadder = true;
            targetVelocityY = Mathf.Sign(directionY) * climbLadderSpeed;            
        }
    }

    public void OnMoving(float directionX, float accelerationTime) {
        OnMoving(directionX, accelerationTime, 1f);
    }

    public void OnMoving(float directionX, float accelerationTime, float moveMultiplier) {
        this.targetVelocityX += directionX * moveSpeed * moveMultiplier;
        this.accelerationTime = accelerationTime;
    }

    public void OnPushed(float directionXPushedFrom, float pushSpeed) {
        this.targetVelocityX += -directionXPushedFrom * pushSpeed;
        this.accelerationTime = 0f;
    }

    // --------------------------------------------------------------------------
    // State checker
    // --------------------------------------------------------------------------
    public bool LadderBelow() {
        if (collisions.ladderAvailable) {
            return true;
        } else {
            return false;
        }
    }

    public bool OnLadder() {
        if (collisions.onLadder) {
            return true;
        } else {
            return false;
        }
    }

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

        if (collisions.onLadder) {
            velocity.y = targetVelocityY;
        } else {
            velocity.y += targetVelocityY + gravity * Time.deltaTime;
        }

        // hier schnelleres Fallen an velocity.y manipulieren.
        if (IsFalling()) {
            velocity.y *= comicFallFactor;
        }
    }
}
