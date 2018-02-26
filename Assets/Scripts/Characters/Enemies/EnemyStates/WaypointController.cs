using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates {
    public class WaypointController{

        private Vector3 wpLeft = Vector3.negativeInfinity;
        private Vector3 wpRight = Vector3.negativeInfinity;

        private bool runLeft;

        // Use this for initialization
        public WaypointController(Vector3 wpLeft, Vector3 wpRight, bool startRunningLeft = true) {
            this.wpLeft = wpLeft;
            this.wpRight = wpRight;
            runLeft = startRunningLeft;
        }
        
        public bool HasWaypoints() {
            if (wpLeft.Equals(Vector3.negativeInfinity) && wpRight.Equals(Vector3.negativeInfinity)) {
                return false;
            } else {
                return true;
            }
        }

        public float GetDirectionXnextWaypoint(Vector3 currentPosition) {            
            if (runLeft) {
                // check auf linken Waypoint
                float distWp = currentPosition.x - wpLeft.x;
                if (distWp <= 0) {
                    //drüber, also WP wechseln
                    runLeft = false;
                    return 1;
                } else {
                    return -1;
                }
            } else {
                // check auf rechten Waypoint
                float distWp = wpRight.x - currentPosition.x;
                if (distWp <= 0) {
                    //drüber, also WP wechseln
                    runLeft = true;
                    return -1;
                } else {
                    return 1;
                }
            }
        }

        public bool IsNearestWaypointLeft(Vector3 currentPosition) {
            float distLeft = currentPosition.x - wpLeft.x;
            float distRight = wpRight.x - currentPosition.x;

            if (distLeft < distRight) {
                return true;
            } else {
                return false;
            }
        }
    }
}
