using UnityEngine;
using System.Collections;

namespace EnemyStates {
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(CharacterMovementController))]
    public class EnemyStateMachine: MonoBehaviour {

        public float observerDistanceNear = 2f;
        public float observerDistanceFar = 6f;

        public static IdleState idleState;
        public static RunningState runningState;



        // current States
        AbstractState currentState;
        [HideInInspector]
        public float currentDirectionX;
        [HideInInspector]
        public Transform currentTransform;
        [HideInInspector]
        public bool isAware = false;

        Animator animator;
        SpriteRenderer spriteRenderer;
        CharacterMovementController characterMovementController;
        ObserverController observerController;

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

            currentState = idleState;
            currentDirectionX = (spriteRenderer.flipX ? 1 : -1); // Enemies always initially face left
        }

        void Update() {
            currentTransform = transform;

            float observeAroundDistance = (isAware ? observerDistanceFar : observerDistanceNear);
            ObserverController.PlayerDetectionInfo detection = observerController.ObserveAround(observeAroundDistance);
            if (detection.distance < 0) {
                // Player near around not detected
                detection = observerController.ObserveDirection(observerDistanceFar, currentDirectionX, 0);
            }

            if (detection.distance < 0) {
                // Player not be found. Aware is false again
                isAware = false;
            } else {
                isAware = true;
            }

                AbstractState newState = currentState.HandleUpdate(this, animator, characterMovementController, detection);
            if (newState != null) {
                currentState.OnExit(this, animator, characterMovementController, detection);
                currentState = newState;
                currentState.OnEnter(this, animator, characterMovementController, detection);
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
    }
}