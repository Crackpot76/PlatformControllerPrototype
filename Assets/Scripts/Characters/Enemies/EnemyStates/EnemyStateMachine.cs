using UnityEngine;
using System.Collections;

namespace EnemyStates {
    [RequireComponent(typeof(Animator))]
    public class EnemyStateMachine: AbstractCharacterController {
        
        public float attackDistance = 0.7f;
        public float attackEverySeconds = 2;

        public static IdleState idleState;
        public static RunningState runningState;
        public static AttackIdleState attackIdleState;
        public static AttackingState attackingState;


        [HideInInspector]
        public float currentDirectionX;
        [HideInInspector]
        public Transform currentTransform;
        [HideInInspector]
        public ObserverController.DetectionInfo currentDetection;
        
        AbstractState currentState;
        Animator animator;
        ObserverController observerController;

        public override void Start() {
            base.Start();

            animator = GetComponent<Animator>();
            observerController = GetComponent<ObserverController>();
            if (!observerController) {
                Debug.LogError("Kein ObserverController gefunden, Object kann auf nichts reagieren!");
            }

            idleState = new IdleState();
            runningState = new RunningState();
            attackIdleState = new AttackIdleState();
            attackingState = new AttackingState();

            currentState = idleState;
            currentDirectionX = (spriteRenderer.flipX ? 1 : -1); // Enemies always initially face left
        }

        public virtual void Update() {
            //base.Update();


            if (!disableStateMovement) {
                currentTransform = transform;

                currentDetection = observerController.DetectOpponents(currentDirectionX);

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
                spriteRenderer.flipX = (currentDirectionX > 0);
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

        public override float GetCurrentAttackDetails() {           
            if (currentState.Equals(attackingState)) {
                return 1f;
            }
            return -1f;
        }
    }
}