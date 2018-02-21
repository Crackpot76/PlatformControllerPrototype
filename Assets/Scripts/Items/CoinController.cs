using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController: MonoBehaviour {

    Animator animator;
    const string COLLECTED = "COLLECTED";
    bool collected = false;
   public AudioClip clip;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("COLLIDER COIN!");
        if (collision.gameObject.tag == "Player") {
            collected = true;
            animator.SetBool(COLLECTED, collected);
            SoundManager.SetSFXVolume(0.7f);
            SoundManager.PlaySFX(clip);
        }
    }

    public void EnterAnimationCollectedFinished() {
        Destroy(gameObject);
    }
}
