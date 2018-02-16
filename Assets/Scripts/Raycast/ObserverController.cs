using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverController: RaycastController {

    public float observerDistanceNear = 2f;
    public float observerDistanceFar = 6f;
    public string[] detectionTags;
    
    private DetectionInfo detectionInfo;

    public override void Start() {
        base.Start();
    }
    

    public DetectionInfo DetectOpponents(float currentDirectionX) {

        float observeAroundDistance = (detectionInfo.isAware ? observerDistanceFar : observerDistanceNear);
        DetectionInfo currentDetection = ObserveAround(observeAroundDistance);
        if (currentDetection.distance < 0) {
            // Player near around not detected
            currentDetection = ObserveDirection(observerDistanceFar, currentDirectionX, 0);
        }

        if (currentDetection.distance < 0) {
            // Player not be found. Aware is false again
            detectionInfo.isAware = false;
        } else {
            detectionInfo.isAware = true;
        }

        return currentDetection;
    }

    private DetectionInfo ObserveDirection(float distance, float directionX, float directionY) {
        UpdateRaycastOrigins();
        detectionInfo.Reset();
        Observe(distance, directionX, directionY);
        return detectionInfo;

    }

    private DetectionInfo ObserveAround(float distance) {
        UpdateRaycastOrigins();
        detectionInfo.Reset();
        Observe(distance, 1, 1);
        Observe(distance, -1, 1);
        Observe(distance, 1, -1);
        Observe(distance, -1, -1);
        return detectionInfo;
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
            
            if (hit && IsGameObjectDetectable(hit.transform.gameObject)) {

                detectionInfo.distance = hit.distance;
                detectionInfo.right = directionX == 1;
                detectionInfo.left = directionX == -1;

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


            if (hit && IsGameObjectDetectable(hit.transform.gameObject)) {

                detectionInfo.distance = hit.distance;
                detectionInfo.below = directionY == -1;
                detectionInfo.above = directionY == 1;
            }
        }
    }

    private bool IsGameObjectDetectable(GameObject go) {
        if (go) {
            foreach(string detectionTag in detectionTags) {
                if (go.CompareTag(detectionTag)) {
                    return true;
                }
            }
        }
        return false;
    }


    public struct DetectionInfo {
        public bool above, below;
        public bool left, right;
        public bool isAware;
        public float distance;

        public void Reset() {
            above = below = false;
            left = right = false;
            distance = -1;
        }
    }
}
