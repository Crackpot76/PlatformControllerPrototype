using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

    public Text healthDisplay;

    public int maxHealth = 1;

    public float currentHealth = 1f;

    public void RefreshDisplay() {
        if (healthDisplay) {
            healthDisplay.text = "Health: " + currentHealth + " / " + maxHealth;
        }
    }


    public void AddMaxHealth(int additionalMaxHealth) {
        maxHealth += additionalMaxHealth;
        RefreshDisplay();
    }

    public void AddHealth(float additionalHealth) {
        if ((currentHealth + additionalHealth) > maxHealth) {
            currentHealth = maxHealth;
        } else {
            currentHealth += additionalHealth;
        }
        RefreshDisplay();
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        RefreshDisplay();
    }

    public bool IsAlive() {
        return (currentHealth > 0);
    }
}
