using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class PlayerController: MonoBehaviour {

    public float moveSpeed = 6;
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    public float comicFallFactor = 1.08f;


    // calculated move variables
    float gravity;
    float jumpForceTimerStart;
    Vector3 velocity;
    float velocityXSmoothing;

    // move variables for next update
    float targetVelocityX;
    float accelerationTime;

    // 2D Environment Controller
    Controller2D controller;

    // SpriteRenderer 
    SpriteRenderer spriteRenderer;

    // Effects
    SpriteFlashing spriteFlashingEffect;

    // actual state variables of Player
    bool disableInput = false;




    void Start() {
        controller = GetComponent<Controller2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteFlashingEffect = new SpriteFlashing(spriteRenderer);
        

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
    }



    void Update() {
        spriteFlashingEffect.Update();
        CalculateVelocity();

        controller.Move(velocity * Time.deltaTime);

        if (controller.collisions.above || controller.collisions.below) {
            if (controller.collisions.slidingDownMaxSlope) {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            } else {
                velocity.y = 0;
            }
        }
        if (!disableInput) {
            targetVelocityX = 0;
        } else {
            disableInput = false;
        }
    }


    public void ReceiveDamage(float directionHitX) {
        
            Debug.Log("Taking Damage!");
            spriteFlashingEffect.StartFlashing(.3f);
            OnMoving(-directionHitX, 0f, 5f); // Push back in oposite direction
            disableInput = true;
       
    }


    public void OnJumpInputDown() {
        // start counting timer for force jump
        jumpForceTimerStart = Time.time;
    }

    // if bool = false, jump is not possible
    public bool OnJumpInputUp() {
        if (IsJumpingPossible()) {
            float newJumpVelocity = CalculateJumpVelocity();
            velocity.y = newJumpVelocity;
            return true;
        } else {
            return false;
        }        
    }

    private float CalculateJumpVelocity() {
        float jumpForceTimerEnd = Time.time;
        float jumpForceTime = jumpForceTimerEnd - jumpForceTimerStart;
        Debug.Log(jumpForceTime);

        // formula for calculating height where 0.1 ms = minJumpHeight und 0.6 ms = maxJumpHeght
        float targetJumpHeight = 4 * jumpForceTime + 2.1f;
        Debug.Log("TargetJumpHeight:" + targetJumpHeight);
        if (targetJumpHeight < minJumpHeight) {
            targetJumpHeight = minJumpHeight;
        }

        if (targetJumpHeight > maxJumpHeight) {
            targetJumpHeight = maxJumpHeight;
        }

        float calculatedGravity = -(2 * targetJumpHeight) / Mathf.Pow(timeToJumpApex, 2);

        return Mathf.Abs(calculatedGravity) * timeToJumpApex;
    }

    private bool IsJumpingPossible() {
        if (!disableInput) {
            if (controller.collisions.below) {
                if (!controller.collisions.slidingDownMaxSlope) {  // no jumping while sliding down                    
                    return true;
                }
            }
        }
        return false;
    }

    public void OnMoving(float directionX, float accelerationTime, float moveSpeedFactor) {
        if (!disableInput) {
            this.targetVelocityX = directionX * moveSpeed * moveSpeedFactor;
            this.accelerationTime = accelerationTime;
        }
    }


    public bool IsGrounded() {
        if (controller.collisions.below) {
            return true;
        } else {
            return false;
        }
    }
    public bool IsFalling() {
        if (velocity.y < 0 && !controller.collisions.below) {
            return true;
        } else {
            return false;
        }
    }

    void CalculateVelocity() {

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTime);

        
        velocity.y += gravity * Time.deltaTime;

        // hier ggf. schnelleres Fallen an velocity.y manipulieren.
        if (velocity.y < 0 && !controller.collisions.below) {
            velocity.y *= comicFallFactor;
        }
    }
}
