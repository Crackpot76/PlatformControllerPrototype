using UnityEngine;
using System.Collections;

namespace EnemyStates {
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(CharacterMovementController))]
    public class EnemyStateMachine: MonoBehaviour {

        public float observerDistanceNear = 2f;
        public float observerDistanceFar = 6f;
        public float attackDistance = 0.7f;
        public float attackEverySeconds = 2;

        public static IdleState idleState;
        public static RunningState runningState;
        public static AttackIdleState attackIdleState;
        public static AttackingState attackingState;



        // current States
        AbstractState currentState;
        [HideInInspector]
        public float currentDirectionX;
        [HideInInspector]
        public Transform currentTransform;
        [HideInInspector]
        public bool isAware = false;
        [HideInInspector]
        public bool disabled = false;

        Animator animator;
        SpriteRenderer spriteRenderer;
        CharacterMovementController characterMovementController;
        ObserverController observerController;
        ObserverController.PlayerDetectionInfo currentDetection;

        void Start() {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            characterMovementController = GetComponent<CharacterMovementController>();
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

        void Update() {
            if (!disabled) {
                currentTransform = transform;

                float observeAroundDistance = (isAware ? observerDistanceFar : observerDistanceNear);
                currentDetection = observerController.ObserveAround(observeAroundDistance);
                if (currentDetection.distance < 0) {
                    // Player near around not detected
                    currentDetection = observerController.ObserveDirection(observerDistanceFar, currentDirectionX, 0);
                }

                if (currentDetection.distance < 0) {
                    // Player not be found. Aware is false again
                    isAware = false;
                } else {
                    isAware = true;
                }

                AbstractState newState = currentState.HandleUpdate(this, animator, characterMovementController, currentDetection);
                if (newState != null) {
                    currentState.OnExit(this, animator, characterMovementController, currentDetection);
                    currentState = newState;
                    currentState.OnEnter(this, animator, characterMovementController, currentDetection);
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
            if (isAware && currentDetection.distance <= attackDistance) {
                // facing in right direction?
                if ((currentDirectionX == 1 && currentDetection.right) ||
                    (currentDirectionX == -1 && currentDetection.left)) {
                    // is in position! May Attack
                    return true;
                }
            }
            return false;
        }
    }
}