using UnityEngine;
using System.Collections;

namespace PlayerStates {
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(PlayerMovementController))]
    public class PlayerStateMachine : MonoBehaviour {

        public static IdleState idleState = new IdleState();
        public static PreJumpIdleState preJumpIdleState = new PreJumpIdleState();
        public static JumpStartIdleState jumpStartIdleState = new JumpStartIdleState();
        public static PreJumpRunningState preJumpRunningState = new PreJumpRunningState();
        public static JumpStartRunningState jumpStartRunningState = new JumpStartRunningState();
        public static FallingState fallingState = new FallingState();
        public static LandIdleState landIdleState = new LandIdleState();
        public static LandRunningState landRunningState = new LandRunningState();
        public static LandRollingState landRollingState = new LandRollingState();
        public static RunningState runningState = new RunningState();
        public static StoppingState stoppingState = new StoppingState();
        public static DuckingState duckingState = new DuckingState();

        // current States
        AbstractState currentState;
        [HideInInspector]
        public float currentDirectionX;
        public Transform currentTransform;

        Animator animator;
        SpriteRenderer spriteRenderer;
        PlayerMovementController playerController;


        void Start() {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            playerController = GetComponent<PlayerMovementController>();
            currentState = idleState;            
            currentDirectionX = 1;
        }

        void Update() {
            currentTransform = transform;
            AbstractState newState = currentState.HandleUpdate(this, animator, playerController);
            if (newState != null) {
                currentState.OnExit(this, animator, playerController);
                currentState = newState;
                currentState.OnEnter(this, animator, playerController);
            }

        }

        public void EventTrigger(string parameter) {
            currentState.OnAnimEvent(parameter);
        }

        public void FlipSprite(float newDirectionX) {
            if (newDirectionX != 0 && currentDirectionX != newDirectionX) {
                currentDirectionX = newDirectionX;
                spriteRenderer.flipX = (currentDirectionX < 0);
            }
        }      
    }
}