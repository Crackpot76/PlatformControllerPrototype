using UnityEngine;
using System.Collections;

namespace PlayerStates {
    [RequireComponent(typeof(Animator))]
    public abstract class PlayerStateMachine : AbstractCharacterController {

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
        public static AttackingLightState attackingLightState;

        [HideInInspector]
        public float currentDirectionX;
        [HideInInspector]
        public Transform currentTransform;
        
        AbstractState currentState;
        Animator animator;

        public override void Start() {
            base.Start();
            animator = GetComponent<Animator>();

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
            attackingLightState = new AttackingLightState();

            currentState = idleState;            
            currentDirectionX = 1;
        }

        public virtual void Update() {
           // base.Update();

            if (!disableStateMovement) { 
                currentTransform = transform;
                AbstractState newState = currentState.HandleUpdate(this, animator, movementController);
                if (newState != null) {
                    currentState.OnExit(this, animator, movementController);
                    currentState = newState;
                    currentState.OnEnter(this, animator, movementController);
                }
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

        public override AttackDetails GetCurrentAttackDetails() {
            AbstractStateAttack instanceCheck = currentState as AbstractStateAttack;
            if (instanceCheck != null) {
                return instanceCheck.GetAttackDetails();
            }
            return null;
        }
    }
}