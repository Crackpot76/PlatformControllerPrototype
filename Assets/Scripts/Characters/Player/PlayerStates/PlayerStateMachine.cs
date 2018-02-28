using UnityEngine;
using System.Collections;

namespace PlayerStates {
    [RequireComponent(typeof(CharacterMovementController), typeof(Animator))]
    public class PlayerStateMachine : MonoBehaviour, IStateMachine {

        public SoundProperties sounds;

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
        public CharacterMovementController movementController;

        bool disableStateMovement = false;
        AbstractState currentState;
        Animator animator;
        GameObject effectGO;
        Cam camerascript;



        public void Start() {
            effectGO = GameObject.Find("Effects");
            if (effectGO == null) {
                effectGO = new GameObject("Effects");
            }
            animator = GetComponent<Animator>();
            movementController = GetComponent<CharacterMovementController>();
            camerascript = GameObject.FindObjectOfType<Cam>();

            if (camerascript) {
                Debug.Log("Cam found!");
            }

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
                AbstractState newState = currentState.HandleUpdate(this, animator, movementController);
                if (newState != null) {
                    currentState.OnExit(this, animator, movementController);
                    currentState = newState;
                    currentState.OnEnter(this, animator, movementController);
                }
            }
        }


        public void FlipSprite(float newDirectionX) {
            if (newDirectionX != 0 && currentDirectionX != newDirectionX) {                
                currentDirectionX = newDirectionX;
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        } 
        
        public void InstantiateEffect(Object effectToInstanciate) {
            GameObject dustGo = (GameObject)Instantiate(effectToInstanciate);
            SpriteRenderer effectSpriteRenderer = dustGo.GetComponent<SpriteRenderer>();
            effectSpriteRenderer.flipX = (currentDirectionX < 0);
            dustGo.transform.parent = effectGO.transform;
            dustGo.transform.position = transform.position;

        }

        public void EventTrigger(string parameter) {
            currentState.OnAnimEvent(this, parameter);

            if (parameter.Equals(EventParameters.DAMAGE)) {
                disableStateMovement = true;
                Invoke("ReenableStateMovement", .1f);
            }
        }
        // Enables state movement after certain time
        private void ReenableStateMovement() {
            disableStateMovement = false;
        }

        public AttackDetails GetCurrentAttackDetails() {
            AbstractStateAttack instanceCheck = currentState as AbstractStateAttack;
            if (instanceCheck != null) {
                return instanceCheck.GetAttackDetails();
            }
            return null;
        }

        public void ShakeCamera(float amplitude, float frequency, float time) {
            StartCoroutine(camerascript.ShakeCoroutine(amplitude, frequency, time));
        }


    }
}