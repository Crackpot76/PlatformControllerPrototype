using UnityEngine;
using System.Collections;

namespace EnemyStates {
    [RequireComponent(typeof(Animator))]
    public class EnemyStateMachine: MonoBehaviour, IStateMachine {

        public Vector3 waypointLeft = Vector3.negativeInfinity;
        public Vector3 waypointRight = Vector3.negativeInfinity;



        public BasicSoundProperties sounds;

        public float attackDistance = 0.7f;
        public float attackEverySeconds = 2;

        // States
        public AbstractState currentState;

        public IdleState idleState = new IdleState();
        public RunningState runningState = new RunningState();
        public AttackIdleState attackIdleState = new AttackIdleState();
        public AttackingState attackingState = new AttackingState();
        public DamageState damageState = new DamageState();
        public DeathState deathState = new DeathState();
        public DecapitateState decapitateState = new DecapitateState();
        
        public WaypointController waypointController;
        public ObserverController.DetectionInfo currentDetection;

        [HideInInspector]
        public float currentDirectionX;
        
        private Vector3 globalWaypointLeft;
        private Vector3 globalWaypointRight;

        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private ObserverController observerController;
        private CharacterMovementController movementController;

        

        public void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            observerController = GetComponent<ObserverController>();
            movementController = GetComponent<CharacterMovementController>();

            currentState = idleState;
            currentDirectionX = -1; // Enemies always initially face left

            if (!waypointLeft.Equals(Vector3.negativeInfinity) && !waypointRight.Equals(Vector3.negativeInfinity)) {
                globalWaypointLeft = waypointLeft + transform.position;
                globalWaypointRight = waypointRight + transform.position;

                waypointController = new WaypointController(globalWaypointLeft, globalWaypointRight);
            }
        }

        public virtual void Update() {
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

        void OnDrawGizmos() {
            if (!waypointLeft.Equals(Vector3.negativeInfinity) && !waypointRight.Equals(Vector3.negativeInfinity)) {
                Gizmos.color = Color.blue;
                float size = .3f;

                Vector3 globalWaypointPos = (Application.isPlaying) ? globalWaypointLeft : waypointLeft + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);

                globalWaypointPos = (Application.isPlaying) ? globalWaypointRight : waypointRight + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
            }
        }
    }


}