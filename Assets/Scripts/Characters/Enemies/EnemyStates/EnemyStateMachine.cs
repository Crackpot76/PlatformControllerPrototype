using UnityEngine;
using System.Collections;

namespace EnemyStates {
    [RequireComponent(typeof(Animator))]
    public class EnemyStateMachine: MonoBehaviour, IStateMachine {
        
        public float attackDistance = 0.7f;
        public float attackEverySeconds = 2;

        public static IdleState idleState;
        public static RunningState runningState;
        public static AttackIdleState attackIdleState;
        public static AttackingState attackingState;
        public static DamageState damageState;
        public static DeathState deathState;


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

            idleState = new IdleState();
            runningState = new RunningState();
            attackIdleState = new AttackIdleState();
            attackingState = new AttackingState();
            damageState = new DamageState();
            deathState = new DeathState();

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

        public void InstantiateEffect(Object effectToInstanciate) {
            GameObject dustGo = (GameObject)Instantiate(effectToInstanciate);
            SpriteRenderer effectSpriteRenderer = dustGo.GetComponent<SpriteRenderer>();
            effectSpriteRenderer.flipX = (currentDirectionX < 0);
            dustGo.transform.position = transform.position;
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
            StartCoroutine(FadeOut());
        }


        private IEnumerator FadeOut() {
            for (float i = 1; i > 0; i -= 0.01f) {
                Color c = spriteRenderer.color;
                c.a = i;
                spriteRenderer.color = c;
                Debug.Log(spriteRenderer.color.a);
                yield return new WaitForSeconds(.1f);
            }
            Destroy(gameObject);
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