using UnityEngine;

public class AttackController : MonoBehaviour {

    private AbstractCharacterController parentCharacterController;

    private void Start() {
        parentCharacterController = transform.parent.gameObject.GetComponent<AbstractCharacterController>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        
        foreach (string opponentTag in parentCharacterController.opponentTags) {
            if (collision.gameObject.tag == opponentTag) {
                float hitDirectionX = GetHitDirection(parentCharacterController.transform, collision);

                AbstractCharacterController opponentCharacterController = collision.gameObject.GetComponent<AbstractCharacterController>();

                if (opponentCharacterController) {
                    if (parentCharacterController.GetCurrentAttackDetails() > 0) {
                       opponentCharacterController.ReceiveDamage(hitDirectionX, parentCharacterController.GetCurrentAttackDetails());
                    }
                } else {
                    Debug.LogError("No PlayerController Script for player found. No damage served today!");
                }
            }
        }
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
