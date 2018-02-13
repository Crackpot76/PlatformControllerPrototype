using UnityEngine;
using System.Collections;

namespace PlayerStates {
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(CharacterMovementController))]
    public class PlayerStateMachine : MonoBehaviour {

        public static IdleState idleState;
        public static PreJumpIdleState preJumpIdleState;
        public static JumpStartIdleState jumpStartIdleState;
        public static PreJumpRunningState preJumpRunningState;
        public static JumpStartRunningState jumpStartRunningState;
        public static FallingState fallingState;
        public static LandIdleState landIdleState;
        public static LandRunningState landRunningState;
        public static LandRollingState landRollingState;
        public static RunningState runningState;
        public static StoppingState stoppingState;
        public static DuckingState duckingState;
        public static DuckingUpState duckingUpState;
        



        // current States
        AbstractState currentState;
        [HideInInspector]
        public float currentDirectionX;
        [HideInInspector]
        public Transform currentTransform;
        [HideInInspector]
        public bool disableUserInput;

        Animator animator;
        SpriteRenderer spriteRenderer;
        CharacterMovementController characterMovementController;


        void Start() {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            characterMovementController = GetComponent<CharacterMovementController>();

            idleState = new IdleState();
            preJumpIdleState = new PreJumpIdleState();
            jumpStartIdleState = new JumpStartIdleState();
            preJumpRunningState = new PreJumpRunningState();
            jumpStartRunningState = new JumpStartRunningState();
            fallingState = new FallingState();
            landIdleState = new LandIdleState();
            landRunningState = new LandRunningState();
            landRollingState = new LandRollingState();
            runningState = new RunningState();
            stoppingState = new StoppingState();
            duckingState = new DuckingState();
            duckingUpState = new DuckingUpState();

            currentState = idleState;            
            currentDirectionX = 1;
        }

        void Update() {
            currentTransform = transform;
            AbstractState newState = currentState.HandleUpdate(this, animator, characterMovementController);
            if (newState != null) {
                currentState.OnExit(this, animator, characterMovementController);
                currentState = newState;
                currentState.OnEnter(this, animator, characterMovementController);
            }

        }

        public void EventTrigger(string parameter) {
            currentState.OnAnimEvent(this, parameter);
        }

        public void FlipSprite(float newDirectionX) {
            if (newDirectionX != 0 && currentDirectionX != newDirectionX) {
                currentDirectionX = newDirectionX;
                spriteRenderer.flipX = (currentDirectionX < 0);
            }
        } 
        
        public void InstantiateEffect(Object effectToInstanciate) {
            GameObject dustGo = (GameObject)Instantiate(effectToInstanciate);
            SpriteRenderer effectSpriteRenderer = dustGo.GetComponent<SpriteRenderer>();
            effectSpriteRenderer.flipX = (currentDirectionX < 0);
            dustGo.transform.position = transform.position;
        }
    }
}