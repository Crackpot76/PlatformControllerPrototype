using UnityEngine;

public class AttackController : MonoBehaviour {

    private AbstractCharacterController parentCharacterController;
    private IStateMachine parentStateMachine;

    private void Start() {
        parentCharacterController = transform.parent.gameObject.GetComponent<AbstractCharacterController>();
        if (parentCharacterController == null) {
            Debug.LogError("parentCharacterController in Attack Controller not found!");
        }
        parentStateMachine = transform.parent.gameObject.GetComponent<IStateMachine>();
        if (parentStateMachine == null) {
            Debug.LogError("parentStateMachine in Attack Controller not found!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {

        Debug.Log("CollisionEnter");

        AttackDetails attack = parentStateMachine.GetCurrentAttackDetails();
        if (attack == null) {
            Debug.LogError("Parent CharacterController is not in state attacking!");
            return;
        }
        float maxHitContactY = GetMaxHitContactY(collision);
        float hitDirectionX = GetHitDirection(parentCharacterController.transform, collision);

        if (ListContainsTag(parentCharacterController.opponentTags, collision.gameObject.tag)) {
            AbstractCharacterController opponentCharacterController = collision.gameObject.GetComponent<AbstractCharacterController>();

            if (opponentCharacterController) {
                opponentCharacterController.ReceiveDamage(hitDirectionX, maxHitContactY, attack);
            } else {
                Debug.LogError("No AbstractCharacterController Script for player found. No damage served today!");
            }
        }

        if (ListContainsTag(parentCharacterController.destructableTags, collision.gameObject.tag))
        {
            AbstractDestructableController destructableController = collision.gameObject.GetComponent<AbstractDestructableController>();

            if (destructableController) {
                destructableController.ReceiveDamage(hitDirectionX, maxHitContactY, attack);
            } else {
                Debug.LogError("No AbstractDestructableController Script for player found. No damage served today!");
            }
        }
    }

    public static bool ListContainsTag(string[] tagList, string tag) {

        foreach(string searchTag in tagList) {
            if (searchTag.Contains(tag)) {
                return true;
            }
        }
        return false;
    }

    public static float GetMaxHitContactY(Collision2D collision) {

        ContactPoint2D[] contactPoints = collision.contacts;
        float result = 0;
        bool first = true;
        foreach (ContactPoint2D contactPoint in contactPoints) {
            if (first) {
                result = contactPoint.point.y;
                first = false;
            } else {
                if (contactPoint.point.y > result) {
                    result = contactPoint.point.y;
                }
            }
        }
        return result;
    }

    public static float GetHitDirection(Transform thisTransform, Collision2D collision) {
        
        if (thisTransform) {
            if (collision.transform.position.x > thisTransform.position.x) {
                // attack from the left
                return -1;
            }
            if (collision.transform.position.x < thisTransform.position.x) {
                // attack from the right
                return 1;
            }
        }

        return 0;
    } 
}
