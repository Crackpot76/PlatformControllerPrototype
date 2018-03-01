using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController {

    const float MAX_LIGHT_ATTACK = 0.2f;
    const float MAX_ATTACK_DOUBLE_TAB_TIME = 0.2f;

    float timeLastAttackDown;
    float timeLastAttackUp;
    float timeBetweenAttackDowns;

    public void MonitorInput() {
        if (Input.GetKeyDown(KeyCode.T)) {            
            timeBetweenAttackDowns = Time.time - timeLastAttackDown;
            timeLastAttackDown = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.T)) {
            timeLastAttackUp = Time.time;
        }
    }

	public float GetMoveDirection() {
        return Input.GetAxisRaw("Horizontal");
    }

    public bool IsJumpPressed() {
        return Input.GetKey(KeyCode.Space);
    }

    public bool IsUpPressed() {
        return Input.GetKey(KeyCode.UpArrow);
    }

    public bool IsDownPressed() {
        return Input.GetKey(KeyCode.DownArrow);
    }

    public bool StartHeavyAttack() {
        if (Input.GetKey(KeyCode.T)) {
            // Calculate Attack duration
            if ((Time.time - timeLastAttackDown) >= MAX_LIGHT_ATTACK) {
                
                return true;
            }
        }
        return false;
    }

    public bool IsLightAttack() {
        if (Input.GetKeyUp(KeyCode.T)) {
            // Calculate Attack duration
            if ((timeLastAttackUp - timeLastAttackDown) < MAX_LIGHT_ATTACK) {
                return true;
            }
        }
        return false;
    }

    public bool IsDoubleTabAttack() {
        if (Input.GetKeyUp(KeyCode.T)) {
            // Calculate Attack duration
            Debug.Log("DoubleTap:" + timeBetweenAttackDowns);
            if (timeBetweenAttackDowns < MAX_ATTACK_DOUBLE_TAB_TIME) {
                Debug.Log("DOUBLE TAB!");
                return true;
            }
        }
        return false;
    }
}
