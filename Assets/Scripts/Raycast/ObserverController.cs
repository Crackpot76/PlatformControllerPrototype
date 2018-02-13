using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverController: RaycastController {

    public string detectionTag;

    public PlayerDetectionInfo playerDetectionInfo;

    public override void Start() {
        base.Start();
    }
    

    public PlayerDetectionInfo ObserveDirection(float distance, float directionX, float directionY) {
        UpdateRaycastOrigins();
        playerDetectionInfo.Reset();
        Observe(distance, directionX, directionY);
        return playerDetectionInfo;

    }

    public PlayerDetectionInfo ObserveAround(float distance) {
        UpdateRaycastOrigins();
        playerDetectionInfo.Reset();
        Observe(distance, 1, 1);
        Observe(distance, -1, 1);
        Observe(distance, 1, -1);
        Observe(distance, -1, -1);
        return playerDetectionInfo;
    }


    private void Observe(float distance, float directionX, float directionY) {
        if (directionX != 0) {
            HorizontalDetection(distance, directionX);
        }

        if (directionY != 0) {
            VerticalDetection(distance, directionY);
        }
    }


    void HorizontalDetection(float distance, float directionX) {
        float rayLength = Mathf.Abs(distance) + skinWidth;

        if (Mathf.Abs(distance) < skinWidth) {
            rayLength = 2 * skinWidth;
        }

        for (int i = 0; i < horizontalRayCount; i++) {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            
            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.blue);
            
            if (hit && hit.transform.gameObject.CompareTag(detectionTag)) {

                playerDetectionInfo.distance = hit.distance;
                playerDetectionInfo.right = directionX == 1;
                playerDetectionInfo.left = directionX == -1;

            }
        }
    }

    void VerticalDetection(float distance, float directionY) {
        float rayLength = Mathf.Abs(distance) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++) {

            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.blue);


            if (hit && hit.transform.gameObject.CompareTag(detectionTag)) {

                playerDetectionInfo.distance = hit.distance;
                playerDetectionInfo.below = directionY == -1;
                playerDetectionInfo.above = directionY == 1;
            }
        }
    }


    public struct PlayerDetectionInfo {
        public bool above, below;
        public bool left, right;
        public float distance;

        public void Reset() {
            above = below = false;
            left = right = false;
            distance = -1;
        }
    }
}
