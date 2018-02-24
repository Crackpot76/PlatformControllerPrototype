using UnityEngine;
using System.Collections;

namespace EnemyStates {
    [RequireComponent(typeof(Animator))]
    public class EnemyStateMachine: MonoBehaviour, IStateMachine {
        
        public float attackDistance = 0.7f;
        public float attackEverySeconds = 2;

        public static IdleState idleState = new IdleState();
        public static RunningState runningState = new RunningState();
        public static AttackIdleState attackIdleState = new AttackIdleState();
        public static AttackingState attackingState = new AttackingState();
        public static DamageState damageState = new DamageState();
        public static DeathState deathState = new DeathState();
        public static DecapitateState decapitateState = new DecapitateState();


        [HideInInspector]
        public float currentDirectionX;
        [HideInInspector]
        public ObserverController.DetectionInfo currentDetection;
        
        AbstractState currentState;

        SpriteRenderer spriteRenderer;
        Animator animator;
        ObserverController observerController;
        CharacterMovementController movementController;

        public void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            observerController = GetComponent<ObserverController>();
            movementController = GetComponent<CharacterMovementController>();

            currentState = idleState;
            currentDirectionX = -1; // Enemies always initially face left
        }

        public virtual void Update() {
            //base.Update();

            currentDetection = observerController.DetectOpponents(currentDirectionX);

            AbstractState newState = currentState.HandleUpdate(this, animator, movementController);
            if (newState != null) {
                currentState.OnExit(this, animator, movementController);
                currentState = newState;
                currentState.OnEnter(this, animator, movementController);
            }
            
        }

        public void FlipSprite(float newDirectionX) {
            if (newDirectionX != 0 && currentDirectionX != newDirectionX) {
                currentDirectionX = newDirectionX;
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }


        public bool InAttackPosition() {
            if (currentDetection.isAware && currentDetection.distance <= attackDistance) {
                // facing in right direction?
                if ((currentDirectionX == 1 && currentDetection.right) ||
                    (currentDirectionX == -1 && currentDetection.left)) {
                    // is in position! May Attack
                    return true;
                }
            }
            return false;
        }

        public void Destroy() {
            GameObject head = transform.Find("DecapHead").gameObject;
            if (head != null && head.activeInHierarchy) {
                SpriteRenderer headSpriteRenderer = head.GetComponent<SpriteRenderer>();
                EffectManager.GetInstance().FadeOutSprite(headSpriteRenderer, 2f, 3f, true);
            }
            EffectManager.GetInstance().FadeOutSprite(spriteRenderer, 2f, 3f, true);
        }

        public void Decapitate() {
            GameObject head = transform.Find("DecapHead").gameObject;
            head.SetActive(true);

            Rigidbody2D rb = head.GetComponent<Rigidbody2D>();
            Vector2 v = new Vector2((-1 * currentDirectionX * 50), 400);
            rb.AddForce(v);

        }

        public void EventTrigger(string parameter) {
            currentState.OnAnimEvent(this, parameter);
        }

        public AttackDetails GetCurrentAttackDetails() {
            AbstractStateAttack instanceCheck = currentState as AbstractStateAttack;
            if (instanceCheck != null) {
                return instanceCheck.GetAttackDetails();
            }
            return null;
        }
    }
}