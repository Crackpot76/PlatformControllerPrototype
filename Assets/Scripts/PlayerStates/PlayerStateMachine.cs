using UnityEngine;
using System.Collections;

namespace PlayerStates {
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(PlayerMovementController))]
    class PlayerStateMachine : MonoBehaviour {

        public static IdleState idleState = new IdleState();
        public static PreJumpIdleState preJumpIdleState = new PreJumpIdleState();
        public static JumpStartIdleState jumpStartIdleState = new JumpStartIdleState();
        public static JumpAirState jumpAirState = new JumpAirState();
        public static PreJumpRunningState preJumpRunningState = new PreJumpRunningState();
        public static JumpStartRunningState jumpStartRunningState = new JumpStartRunningState();
        public static FallingIdleState fallingIdleState = new FallingIdleState();
        public static FallingRunningState fallingRunningState = new FallingRunningState();
        public static LandingIdleState landingIdleState = new LandingIdleState();
        public static LandingRunningState landingRunningState = new LandingRunningState();
        public static RunningState runningState = new RunningState();
        public static StoppingState stoppingState = new StoppingState();

        // current States
        IStateInterface currentState;
        [HideInInspector]
        public float currentDirectionX;

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
            IStateInterface newState = currentState.HandleUpdate(this, ref animator, ref playerController);
            if (newState != null) {
                currentState.OnExit(this, ref animator, ref playerController);
                currentState = newState;
                currentState.OnEnter(this, ref animator, ref playerController);
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