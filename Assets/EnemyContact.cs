using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContact : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision) { 

        if (collision.gameObject.tag == "Player") {
            ContactPoint2D[] contactPoints = new ContactPoint2D[2];

            int count = collision.GetContacts(contactPoints);

            Collider2D playerCollider = collision.gameObject.GetComponent<Collider2D>();
            Vector3 centerOfPlayerCollider = playerCollider.bounds.center;

            if (count < 2) {
                Debug.LogError("Contacts in collision < 2. Not enough to calculate side of contact!");
                return;
            }

            float directionHitX = 0;
            //float directionHitY = 0;

            float diffX1 = contactPoints[0].point.x - centerOfPlayerCollider.x;
            float diffY1 = contactPoints[0].point.y - centerOfPlayerCollider.y;

            float diffX2 = contactPoints[1].point.x - centerOfPlayerCollider.x;
            float diffY2 = contactPoints[1].point.y - centerOfPlayerCollider.y;          

            if (diffX1 == diffX2) {
                // contact with X-axis
                directionHitX = Mathf.Sign(diffX1);
            } else if (diffY1 == diffY2) {
                // contact with Y-axis
                // directionHitY = Mathf.Sign(diffY1);
            } else {
                // WEIRD!
            }

            PlayerMovementController playerController = collision.gameObject.GetComponent<PlayerMovementController>();

            if (playerController) {
                playerController.ReceiveDamage(directionHitX);
            } else {
                Debug.LogError("No PlayerController Script for player found. No damage served today!");
            }


        }
    }
}
