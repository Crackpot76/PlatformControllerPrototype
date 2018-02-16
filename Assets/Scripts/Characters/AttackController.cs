using UnityEngine;

public class AttackController : MonoBehaviour {

    private AbstractCharacterController parentCharacterController;

    private void Start() {
        parentCharacterController = transform.parent.gameObject.GetComponent<AbstractCharacterController>();
    }

    void OnCollisionEnter2D(Collision2D collision) {

        if (ListContainsTag(parentCharacterController.opponentTags, collision.gameObject.tag)) {

            float maxHitContactY = GetMaxHitContactY(collision);
            Debug.Log("MaxHitY:" + maxHitContactY);
            float hitDirectionX = GetHitDirection(parentCharacterController.transform, collision);

            AbstractCharacterController opponentCharacterController = collision.gameObject.GetComponent<AbstractCharacterController>();

            if (opponentCharacterController) {
                AttackDetails attack = parentCharacterController.GetCurrentAttackDetails();
                if (attack != null) {
                    opponentCharacterController.ReceiveDamage(hitDirectionX, maxHitContactY, attack);
                } else {
                    Debug.LogError("Parent CharacterController is not in state attacking!");
                }
            } else {
                Debug.LogError("No PlayerController Script for player found. No damage served today!");
            }
        }

        if (ListContainsTag(parentCharacterController.destructableTags, collision.gameObject.tag))
            {
            //TODO handle destructable object code
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
