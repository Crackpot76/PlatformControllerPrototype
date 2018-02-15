using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitContact : MonoBehaviour {


    public string[] opponentTags;
   

    void OnCollisionEnter2D(Collision2D collision) {
        foreach (string opponentTag in opponentTags) {
            if (collision.gameObject.tag == opponentTag) {
                
                float hitDirectionX = GetHitDirection(collision);

                CharacterInterface playerController = collision.gameObject.GetComponent<CharacterInterface>();

                if (playerController) {
                    playerController.ReceiveDamage(hitDirectionX, 1f);
                } else {
                    Debug.LogError("No PlayerController Script for player found. No damage served today!");
                }
            }
        }
    }

    private float GetHitDirection(Collision2D collision) {
        GameObject attackerGo = gameObject;
        Transform parent = transform.parent;
        if (parent && parent.gameObject) {
            // there is a parent, so take this one
            attackerGo = parent.gameObject;
        }

        if (collision.transform.position.x > attackerGo.transform.position.x) {
            // attack from the left
            return -1;
        }
        if (collision.transform.position.x < attackerGo.transform.position.x) {
            // attack from the right
            return 1;
        }
        return 0;
    } 
}
